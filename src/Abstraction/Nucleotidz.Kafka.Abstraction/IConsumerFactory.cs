using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IConsumerFactory<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        IConsumer<TKey, TValue> Create();
    }
}
