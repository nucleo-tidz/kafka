using com.nucleotidz.employee.key;
using com.nucleotidz.employee;
using Consumer.Example;
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IHandler<employeeKey, employeeMessage>, Worker>();
        services.AddConsumer<employeeKey, employeeMessage>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
    })
    .Build();

await host.RunAsync();
