using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Consumer
{
    public class Consumer<TKey, TValue> : BackgroundService 
        where TKey : class
        where TValue: class
    {
        readonly ConsumerConfiguration _consumerConfiguration;
        readonly ISerializerFactory _serializerFactory;
        readonly IHandler<TKey, TValue> _handler;
        public Consumer(IOptions<ConsumerConfiguration> _consumerConfigurationOption, ISerializerFactory serializerFactory, IHandler<TKey, TValue> handler)
        {
            _consumerConfiguration = _consumerConfigurationOption.Value;
            ArgumentNullException.ThrowIfNull(serializerFactory, nameof(serializerFactory));
            ArgumentNullException.ThrowIfNull(serializerFactory, nameof(serializerFactory));
            ArgumentNullException.ThrowIfNull(handler, nameof(handler));
            _serializerFactory = serializerFactory;
            _handler = handler;
        }
        private ConsumerConfig CreateConfiguration()
        {
            return new ConsumerConfig
            {
                BootstrapServers = string.Join(",", _consumerConfiguration.BootstrapServers),
                EnableAutoCommit = false,
                GroupId = _consumerConfiguration.GroupName,
                ClientId = _consumerConfiguration.ClientId,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _consumerConfiguration.Username,
                SaslPassword = _consumerConfiguration.Password,
                AutoOffsetReset = _consumerConfiguration.AutoOffsetReset

            };
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var buffer = new List<ConsumeResult<TKey, TValue>>();
            var lastReset = GetUtcTime();
            using (var consumer =
                new ConsumerBuilder<TKey, TValue>(CreateConfiguration())
                    .SetKeyDeserializer(_serializerFactory.CreateDeserializer<TKey>())
                    .SetValueDeserializer(_serializerFactory.CreateDeserializer<TValue>())
                    .Build())
            {
                consumer.Subscribe(_consumerConfiguration.Topic);
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumedMessage = consumer.Consume(TimeSpan.FromMilliseconds(1000));
                    if (consumedMessage?.Message is not null)
                    {
                        buffer.Add(consumedMessage);
                        var timeSinceLastReset = GetUtcTime() - lastReset;

                        if (buffer.Count < _consumerConfiguration.BatchSize &&
                            timeSinceLastReset.TotalSeconds < _consumerConfiguration.TimeOut)
                        {
                            continue;
                        }
                    }
                    if (!buffer.Any())
                    {
                        continue;
                    }

                    var offsets = await _handler.HandleAsync(buffer, stoppingToken);
                    consumer.Commit(offsets);
                    buffer.Clear();
                    lastReset = GetUtcTime();
                }
            }
        }
        private DateTimeOffset GetUtcTime()
        {
            return DateTimeOffset.UtcNow;
        }

    }
}
