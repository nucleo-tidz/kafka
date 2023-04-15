using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;
using Nucleotidz.Kafka.SchemaRegistry;
using Nucleotidz.Kafka.Serializer;

namespace Nucleotidz.Kafka.Consumer
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddAvroConsumer(this IServiceCollection services)
        {
            services.AddTransient<ISerializerFactory, AvroSerializerFactory>();
            return services;
        }
        private static IServiceCollection AddJsonConsumer(this IServiceCollection services)

        {
            services.AddTransient<ISerializerFactory, JsonSerializerFactory>();
            return services;
        }
        public static IServiceCollection AddConsumer<TKey, TValue>(this IServiceCollection services,
            IConfiguration configuration, string ConsumerConfigurationSection, string SchemaRegistryConfigurationSection, SerializationScheme serializationScheme)
            where TKey : class
            where TValue : class
        {
            services.Configure<ConsumerConfiguration>(configuration.GetSection(ConsumerConfigurationSection));
            services.Configure<SchemaRegistryConfiguration>(configuration.GetSection(SchemaRegistryConfigurationSection));
            services.AddTransient<ISchemaRegistryFactory, SchemaRegistryFactory>();
            services.AddTransient<IConsumerFactory<TKey, TValue>, ConsumerFactory<TKey, TValue>>();
            if (serializationScheme == SerializationScheme.avro)
            {
                services.AddAvroConsumer();
            }
            else if(serializationScheme == SerializationScheme.json)
            {
                services.AddJsonConsumer();
            }
            else
            {
                throw new NotSupportedException();
            }
            services.AddHostedService<Consumer<TKey, TValue>>();
            return services;
        }
    }
}
