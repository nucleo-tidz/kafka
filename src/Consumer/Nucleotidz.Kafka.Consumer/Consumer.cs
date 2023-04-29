using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Consumer
{
    public class Consumer<TKey, TValue> : BackgroundService
        where TKey : class
        where TValue : class
    {
        private readonly ConsumerConfiguration _consumerConfiguration;
        private readonly IHandler<TKey, TValue> _handler;
        private readonly IConsumerFactory<TKey, TValue> _consumerFactory;
        public Consumer(IOptions<ConsumerConfiguration> consumerConfigurationOption, IHandler<TKey, TValue> handler, IConsumerFactory<TKey, TValue> consumerFactory)
        {
            _consumerConfiguration = consumerConfigurationOption.Value;
            _handler = handler;
            _consumerFactory = consumerFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<ConsumeResult<TKey, TValue>> buffer = new();
            DateTimeOffset lastReset = GetUtcTime();
            using IConsumer<TKey, TValue> consumer = _consumerFactory.Create();
            consumer.Subscribe(_consumerConfiguration.Topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumedMessage = consumer.Consume(TimeSpan.FromMilliseconds(1000));
                if (consumedMessage?.Message is not null)
                {
                    buffer.Add(consumedMessage);
                    TimeSpan timeSinceLastReset = GetUtcTime() - lastReset;

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
                var toCommit=PrepareCommit(offsets);
                consumer.Commit(toCommit);
                buffer.Clear();
                lastReset = GetUtcTime();
            }
        }

        private IEnumerable<TopicPartitionOffset> PrepareCommit(IEnumerable<TopicPartitionOffset> consumedOffsets)
        {
            var offsetToCommit = new List<TopicPartitionOffset>();
            var topicPartitionGroup = consumedOffsets.GroupBy(tpo => new
            {
                tpo.Topic,
                tpo.Partition.Value
            });

            foreach (var topicPartition in topicPartitionGroup)
            {
                var offset = topicPartition.OrderBy(o => o.Offset.Value).LastOrDefault();

                if (offset is null)
                {
                    continue;
                }

                offsetToCommit.Add( new TopicPartitionOffset(offset.TopicPartition, offset.Offset + 1));
            }
            return offsetToCommit;
        }

        private DateTimeOffset GetUtcTime()
        {
            return DateTimeOffset.UtcNow;
        }

    }
}
