using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IConsumerErrorHandler<TKey, TValue>
    {
        void Handle(IConsumer<TKey, TValue> consumer, Error error);
    }
}
