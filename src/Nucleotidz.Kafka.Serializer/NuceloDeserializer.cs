﻿using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace Nucleotidz.Kafka.Serializer
{
    internal class NuceloDeserializer<T> : AvroDeserializer<T>, IAsyncDeserializer<T>
    {
        public NuceloDeserializer(ISchemaRegistryClient schemaRegistryClient)
            : base(schemaRegistryClient)
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
