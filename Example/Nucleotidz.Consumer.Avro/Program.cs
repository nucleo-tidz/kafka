using com.nucleotidz.employee.key;
using com.nucleotidz.employee;
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;
using Nucleotidz.Consumer.Avro;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IConsumerErrorHandler<employeeKey, employeeMessage>, ErrorHandler>();
        services.AddTransient<IPartitionsAssignedHandler<employeeKey, employeeMessage>>(_ => default);
        services.AddTransient<IHandler<employeeKey, employeeMessage>, Worker>();
        services.AddConsumer<employeeKey, employeeMessage>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry",SerializationScheme.avro);
    })
    .Build();

await host.RunAsync();
