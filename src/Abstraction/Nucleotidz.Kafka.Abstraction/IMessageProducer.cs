using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IMessageProducer<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        Task<DeliveryResult<TKey, TValue>> Produce(Message<TKey, TValue> message);
    }
}
