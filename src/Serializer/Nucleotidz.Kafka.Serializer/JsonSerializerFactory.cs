using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Kafka.Serializer
{
    public class JsonSerializerFactory : ISerializerFactory
    {
        private readonly ISchemaRegistryClient _schemaRegistryClient;
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