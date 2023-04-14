using com.nucleotidz.employee.key;
using com.nucleotidz.employee;
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;
using Nucleotidz.Consumer.Avro;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IErrorHandler<employeeKey, employeeMessage>, ErrorHandler>();
        services.AddTransient<IPartitionsAssignedHandler<employeeKey, employeeMessage>>(_ => default);
        services.AddTransient<IHandler<employeeKey, employeeMessage>, Worker>();
        services.AddAvroConsumer<employeeKey, employeeMessage>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
    })
    .Build();

await host.RunAsync();
