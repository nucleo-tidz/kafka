
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Producer.Avro;
using Nucleotidz.Kafka.Producer;
using Customer.Orders;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IProducerErrorHandler<CustomerOrderKey, CustomerOrder>>(_ => default);
        services.AddProducer<CustomerOrderKey, CustomerOrder>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry", SerializationScheme.avro);
       
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
