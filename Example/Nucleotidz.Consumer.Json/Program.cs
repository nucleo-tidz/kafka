
using com.nucleotidz.rifle;
using com.nucleotidz.rifle.key;
using Nucleotidz.Consumer.Json;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IHandler<EmployeeKey, Employees>, Worker>();
        services.AddJsonConsumer<EmployeeKey, Employees>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
    })
    .Build();

await host.RunAsync();
