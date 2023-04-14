
using Nucleotidz.Consumer.Json;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IHandler<AnimalKey, Animal>, Worker>();
        services.AddJsonConsumer<AnimalKey, Animal>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
    })
    .Build();

await host.RunAsync();
