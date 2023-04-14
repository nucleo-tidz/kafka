using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface ISerializerFactory
    {
        public ISerializer<T> CreateSerializer<T>() where T : class;

        public IDeserializer<T> CreateDeserializer<T>() where T : class;

    }
}
