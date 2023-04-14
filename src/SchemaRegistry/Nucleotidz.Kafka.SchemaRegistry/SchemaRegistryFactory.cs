using Confluent.SchemaRegistry;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.SchemaRegistry
{
    public class SchemaRegistryFactory : ISchemaRegistryFactory
    {
        private readonly SchemaRegistryConfiguration _schemaRegistryConfiguration;
        public SchemaRegistryFactory(IOptions<SchemaRegistryConfiguration> configuration)
        {
            _schemaRegistryConfiguration = configuration.Value;
        }
        public CachedSchemaRegistryClient Create()
        {
            ArgumentNullException.ThrowIfNull(_schemaRegistryConfiguration, nameof(_schemaRegistryConfiguration));

            if (string.IsNullOrWhiteSpace(_schemaRegistryConfiguration.Url))
            {
                throw new ArgumentNullException(nameof(_schemaRegistryConfiguration.Url));
            }

            if (string.IsNullOrWhiteSpace(_schemaRegistryConfiguration.Username))
            {
                throw new ArgumentNullException(nameof(_schemaRegistryConfiguration.Username));
            }

            if (string.IsNullOrWhiteSpace(_schemaRegistryConfiguration.Password))
            {
                throw new ArgumentNullException(nameof(_schemaRegistryConfiguration.Password));
            }

            SchemaRegistryConfig schemaRegistryConfig = new()
            {
                Url = _schemaRegistryConfiguration.Url,
                BasicAuthUserInfo = $"{_schemaRegistryConfiguration.Username}:{_schemaRegistryConfiguration.Password}"
            };
            return new CachedSchemaRegistryClient(schemaRegistryConfig);
        }
    }
}