using Confluent.SchemaRegistry;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface ISchemaRegistryFactory
    {
        CachedSchemaRegistryClient Create();
    }
}
