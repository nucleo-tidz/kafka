using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IProducerErrorHandler<TKey, TValue>
    {
        void Handle(IProducer<TKey, TValue> producer, Error error);
    }
}
