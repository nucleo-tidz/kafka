using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using Nucleotidz.Kafka.Abstraction;
using Confluent.Kafka.SyncOverAsync;

namespace Nucleotidz.Kafka.Serializer
{
    public class AvroSerializerFactory : ISerializerFactory
    {
        ISchemaRegistryClient _schemaRegistryClient;
        public AvroSerializerFactory(ISchemaRegistryFactory schemaRegistryFactory)
        {
            _schemaRegistryClient = schemaRegistryFactory.Create();
        }
        public  ISerializer<T> CreateSerializer<T>()
        {
            return new AvroSerializer<T>(_schemaRegistryClient).AsSyncOverAsync();
        }

        public  IDeserializer<T> CreateDeserializer<T>()
        {
            return new NuceloDeserializer<T>(_schemaRegistryClient).AsSyncOverAsync();
        }
    } 
}