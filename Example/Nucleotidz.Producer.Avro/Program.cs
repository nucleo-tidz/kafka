using com.nucleotidz.employee;
using com.nucleotidz.employee.key;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Producer.Avro;
using Nucleotidz.Kafka.Producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IProducerErrorHandler<employeeKey, employeeMessage>>(_ => default);
        services.AddProducer<employeeKey, employeeMessage>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry", SerializationScheme.avro);
       
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
