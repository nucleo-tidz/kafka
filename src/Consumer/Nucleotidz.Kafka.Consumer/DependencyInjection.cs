using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;
using Nucleotidz.Kafka.SchemaRegistry;
using Nucleotidz.Kafka.Serializer;

namespace Nucleotidz.Kafka.Consumer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAvroConsumer<TKey, TValue>(this IServiceCollection services,
            IConfiguration configuration, string ConsumerConfigurationSection, string SchemaRegistryConfigurationSection)
             where TKey : class
             where TValue : class
        {
            services.Configure<ConsumerConfiguration>(configuration.GetSection(ConsumerConfigurationSection));
            services.Configure<SchemaRegistryConfiguration>(configuration.GetSection(SchemaRegistryConfigurationSection));
            services.AddTransient<ISchemaRegistryFactory, SchemaRegistryFactory>();
            services.AddTransient<ISerializerFactory, AvroSerializerFactory>();
            services.AddHostedService<Consumer<TKey, TValue>>();
            return services;
        }
        public static IServiceCollection AddJsonConsumer<TKey, TValue>(this IServiceCollection services,
           IConfiguration configuration, string ConsumerConfigurationSection, string SchemaRegistryConfigurationSection)
            where TKey : class
            where TValue : class
        {
            services.Configure<ConsumerConfiguration>(configuration.GetSection(ConsumerConfigurationSection));
            services.Configure<SchemaRegistryConfiguration>(configuration.GetSection(SchemaRegistryConfigurationSection));
            services.AddTransient<ISchemaRegistryFactory, SchemaRegistryFactory>();
            services.AddTransient<ISerializerFactory, JsonSerializerFactory>();
            services.AddHostedService<Consumer<TKey, TValue>>();
            return services;
        }
    }
}
