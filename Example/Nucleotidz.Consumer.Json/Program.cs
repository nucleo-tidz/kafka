
using Nucleotidz.Consumer.Json;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IPartitionsAssignedHandler<AnimalKey, Animal>, PartitionsAssignedHandler>();
        services.AddTransient<IErrorHandler<AnimalKey, Animal>>(_ => default);
        services.AddTransient<IHandler<AnimalKey, Animal>, Worker>();
        services.AddJsonConsumer<AnimalKey, Animal>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
    })
    .Build();

await host.RunAsync();
