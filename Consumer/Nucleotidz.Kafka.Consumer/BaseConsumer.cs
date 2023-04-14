using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Consumer
{
    public abstract class Consumer<TKey, TValue> : BackgroundService
    {
        readonly ConsumerConfiguration _consumerConfiguration;
        ISerializerFactory _serializerFactory;
        public Consumer(IOptions<ConsumerConfiguration> _consumerConfigurationOption, ISerializerFactory serializerFactory)
        {
            _consumerConfiguration = _consumerConfigurationOption.Value;
            ArgumentNullException.ThrowIfNull(serializerFactory, nameof(serializerFactory));
            _serializerFactory = serializerFactory;
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
                    var consumedMessage = consumer.Consume(TimeSpan.FromMilliseconds(_consumerConfiguration.TimeOut));
                    if (consumedMessage?.Message is not null)
                    {
                        buffer.Add(consumedMessage);
                        var timeSinceLastReset = GetUtcTime() - lastReset;

                        if (buffer.Count < _consumerConfiguration.BatchSize &&
                            timeSinceLastReset.TotalSeconds < 10)
                        {
                            continue;
                        }
                    }
                    if (!buffer.Any())
                    {
                        continue;
                    }

                    var offsets = await HandleAsync(buffer, stoppingToken);
                    consumer.Commit(offsets);
                }
            }
        }
        private DateTimeOffset GetUtcTime()
        {
            return DateTimeOffset.UtcNow;
        }
        public abstract Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<TKey, TValue>> consumeResults, CancellationToken cancellationToken);
    }
}
