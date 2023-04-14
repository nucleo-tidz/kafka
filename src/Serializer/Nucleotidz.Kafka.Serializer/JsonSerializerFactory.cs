using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using Nucleotidz.Kafka.Abstraction;
using Confluent.Kafka.SyncOverAsync;

namespace Nucleotidz.Kafka.Serializer
{
    public class JsonSerializerFactory : ISerializerFactory
    {
        ISchemaRegistryClient _schemaRegistryClient;
        public JsonSerializerFactory(ISchemaRegistryFactory schemaRegistryFactory)
        {
            _schemaRegistryClient = schemaRegistryFactory.Create();
        }
        public ISerializer<T> CreateSerializer<T>()
            where T : class
        {
            return new JsonSerializer<T>(_schemaRegistryClient).AsSyncOverAsync();
        }

        public IDeserializer<T> CreateDeserializer<T>() where T : class
        {
            return new JsonDeserializer<T>().AsSyncOverAsync();
        }
    }
}