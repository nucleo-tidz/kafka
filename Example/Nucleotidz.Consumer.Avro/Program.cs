
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Consumer;
using Nucleotidz.Consumer.Avro;
using Customer.Orders;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IConsumerErrorHandler<CustomerOrderKey, CustomerOrder>, ErrorHandler>();
        services.AddTransient<IPartitionsAssignedHandler<CustomerOrderKey, CustomerOrder>>(_ => default);
        services.AddTransient<IHandler<CustomerOrderKey, CustomerOrder>, Worker>();
        services.AddConsumer<CustomerOrderKey, CustomerOrder>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry",SerializationScheme.avro);
    })
    .Build();

await host.RunAsync();
