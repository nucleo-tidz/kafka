using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using NJsonSchema.Generation;

namespace Nucleotidz.Kafka.Serializer
{
    internal class NuceloJsonSerializer<T> : Confluent.SchemaRegistry.Serdes.JsonDeserializer<T>, IAsyncDeserializer<T>
        where T : class
    {
        public NuceloJsonSerializer(JsonSchemaGeneratorSettings? jsonSchemaGeneratorSettings = null, IEnumerable<KeyValuePair<string, string>>? config = null)
            : base(config,jsonSchemaGeneratorSettings)
        { }
        public new Task<T> DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
                return Task.FromResult(default(T));
            else
            {
                return base.DeserializeAsync(data, isNull, context);
            }
        }
    }

}
