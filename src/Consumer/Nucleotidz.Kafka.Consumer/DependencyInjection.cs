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
        public static IServiceCollection AddAvroConsumer<TKey, TValue>(this IServiceCollection services,
            IConfiguration configuration, string ConsumerConfigurationSection, string SchemaRegistryConfigurationSection)
             where TKey : class
             where TValue : class
        {
            services.AddTransient<ISerializerFactory, AvroSerializerFactory>()
                .AddConsumer<TKey, TValue>(configuration, ConsumerConfigurationSection, SchemaRegistryConfigurationSection); 
            return services;
        }
        public static IServiceCollection AddJsonConsumer<TKey, TValue>(this IServiceCollection services,
           IConfiguration configuration, string ConsumerConfigurationSection, string SchemaRegistryConfigurationSection)
            where TKey : class
            where TValue : class
        {
            services.AddTransient<ISerializerFactory, JsonSerializerFactory>()
                .AddConsumer<TKey, TValue>(configuration, ConsumerConfigurationSection, SchemaRegistryConfigurationSection);

            return services;
        }
        private static IServiceCollection AddConsumer<TKey, TValue>(this IServiceCollection services,
            IConfiguration configuration, string ConsumerConfigurationSection, string SchemaRegistryConfigurationSection)
            where TKey : class
            where TValue : class
        {
            services.Configure<ConsumerConfiguration>(configuration.GetSection(ConsumerConfigurationSection));
            services.Configure<SchemaRegistryConfiguration>(configuration.GetSection(SchemaRegistryConfigurationSection));
            services.AddTransient<ISchemaRegistryFactory, SchemaRegistryFactory>();
            services.AddTransient<IConsumerFactory<TKey,TValue>, ConsumerFactory<TKey, TValue>>();
            services.AddHostedService<Consumer<TKey, TValue>>();
            return services;
        }
    }
}
