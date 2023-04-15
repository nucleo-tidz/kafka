using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Producer.Json;
using Nucleotidz.Kafka.Producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IProducerErrorHandler<AnimalKey, Animal>>(_ => default);
        services.AddProducer<AnimalKey, Animal>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry", SerializationScheme.json);
        services.AddHostedService<Worker>();
       
    })
    .Build();

await host.RunAsync();
