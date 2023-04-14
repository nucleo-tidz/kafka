using Confluent.SchemaRegistry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface ISchemaRegistryFactory
    {
        CachedSchemaRegistryClient Create();
    }
}
