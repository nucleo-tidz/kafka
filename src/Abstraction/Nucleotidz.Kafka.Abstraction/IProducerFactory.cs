using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IProducerFactory<TKey, TValue>
        where TKey : class
        where TValue : class
    {
       IProducer<TKey, TValue> Create();
    }
}
