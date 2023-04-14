using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IErrorHandler<TKey, TValue>
    {
        void Handle(IConsumer<TKey, TValue> consumer, Error error);
    }
}
