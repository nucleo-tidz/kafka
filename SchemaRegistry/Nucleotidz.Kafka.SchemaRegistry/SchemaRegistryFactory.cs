namespace Nucleotidz.Kafka.SchemaRegistry
{
    using Confluent.SchemaRegistry;
    using Microsoft.Extensions.Options;
    using Nucleotidz.Kafka.Abstraction;

    public class SchemaRegistryFactory : ISchemaRegistryFactory
    {
        readonly SchemaRegistryConfiguration _schemaRegistryConfiguration;
        public SchemaRegistryFactory(IOptions<SchemaRegistryConfiguration> configuration)
        {
            _schemaRegistryConfiguration = configuration.Value;
        }
        public CachedSchemaRegistryClient Create()
        {
            ArgumentNullException.ThrowIfNull(_schemaRegistryConfiguration,nameof(_schemaRegistryConfiguration));

            if (string.IsNullOrWhiteSpace(_schemaRegistryConfiguration.Url))
                throw new ArgumentNullException(nameof(_schemaRegistryConfiguration.Url));
            if (string.IsNullOrWhiteSpace(_schemaRegistryConfiguration.Username))
                throw new ArgumentNullException(nameof(_schemaRegistryConfiguration.Username));
            if (string.IsNullOrWhiteSpace(_schemaRegistryConfiguration.Password))
                throw new ArgumentNullException(nameof(_schemaRegistryConfiguration.Password));

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = _schemaRegistryConfiguration.Url,
                BasicAuthUserInfo = $"{_schemaRegistryConfiguration.Username}:{_schemaRegistryConfiguration.Password}"
            };
            return new CachedSchemaRegistryClient(schemaRegistryConfig);
        }
    }
}