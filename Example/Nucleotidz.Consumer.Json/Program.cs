
using Nucleotidz.Consumer.Json;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IPartitionsAssignedHandler<AnimalKey, Animal>, PartitionsAssignedHandler>();
        services.AddTransient<IConsumerErrorHandler<AnimalKey, Animal>>(_ => default);
        services.AddTransient<IHandler<AnimalKey, Animal>, Worker>();
        services.AddConsumer<AnimalKey, Animal>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry",SerializationScheme.json);
    })
    .Build();

await host.RunAsync();
