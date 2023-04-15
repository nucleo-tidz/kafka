using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;
using Nucleotidz.Kafka.SchemaRegistry;
using Nucleotidz.Kafka.Serializer;

namespace Nucleotidz.Kafka.Producer
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddAvro(this IServiceCollection services)
        {
            services.AddTransient<ISerializerFactory, AvroSerializerFactory>();
            return services;
        }
        private static IServiceCollection AddJson(this IServiceCollection services)

        {
            services.AddTransient<ISerializerFactory, JsonSerializerFactory>();
            return services;
        }
        public static IServiceCollection AddProducer<TKey, TValue>(this IServiceCollection services,
            IConfiguration configuration, string producerConfigurationSection, string SchemaRegistryConfigurationSection, SerializationScheme serializationScheme)
            where TKey : class
            where TValue : class
        {
            services.Configure<ProducerConfiguration>(configuration.GetSection(producerConfigurationSection));
            services.Configure<SchemaRegistryConfiguration>(configuration.GetSection(SchemaRegistryConfigurationSection));
            services.AddTransient<ISchemaRegistryFactory, SchemaRegistryFactory>();
            services.AddTransient<IProducerFactory<TKey, TValue>, ProducerFactory<TKey, TValue>>();
            if (serializationScheme == SerializationScheme.avro)
            {
                services.AddAvro();
            }
            else if(serializationScheme == SerializationScheme.json)
            {
                services.AddJson();
            }
            else
            {
                throw new NotSupportedException();
            }
            services.AddTransient<IMessageProducer<TKey, TValue>, Producer<TKey, TValue>>();
            return services;
        }
    }
}
