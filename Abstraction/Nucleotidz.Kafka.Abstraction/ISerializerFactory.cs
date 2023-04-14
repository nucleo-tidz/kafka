using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface ISerializerFactory
    {
        public ISerializer<T> CreateSerializer<T>();

        public IDeserializer<T> CreateDeserializer<T>();

    }
}
