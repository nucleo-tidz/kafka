using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Kafka.Serializer
{
    public class AvroSerializerFactory : ISerializerFactory
    {
        private readonly ISchemaRegistryClient _schemaRegistryClient;
        public AvroSerializerFactory(ISchemaRegistryFactory schemaRegistryFactory)
        {
            _schemaRegistryClient = schemaRegistryFactory.Create();
        }
        public ISerializer<T> CreateSerializer<T>()
            where T : class
        {
            return new AvroSerializer<T>(_schemaRegistryClient).AsSyncOverAsync();
        }

        public IDeserializer<T> CreateDeserializer<T>() where T : class
        {
            return new NuceloAvroDeserializer<T>(_schemaRegistryClient).AsSyncOverAsync();
        }
    }
}