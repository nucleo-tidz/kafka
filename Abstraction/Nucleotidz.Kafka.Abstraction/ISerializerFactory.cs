using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface ISerializerFactory
    {
        public ISerializer<T> CreateSerializer<T>();

        public IDeserializer<T> CreateDeserializer<T>();
        
    }
}
