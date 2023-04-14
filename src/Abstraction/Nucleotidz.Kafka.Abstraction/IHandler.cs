using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IHandler<TKey, TValue>
    {
        Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<TKey, TValue>> consumeResults, CancellationToken cancellationToken);
    }
}
