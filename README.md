# kafka-consumer


A light weight nuget package to consume events from a kafka topic written on .NET 6.0
> Follow this to install nuget package in your project - https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry
```
  "Kafka": {
    "Username": "<SASL USERNAME>",
    "Password": "<SASL PASSWORD>",
    "BootstrapServers": [
      "<Boostrap Server Address>"
    ],
    "ClientId": "<Client ID>",
    "GroupName": "<Consumer Group Id>",
    "BatchSize": 700,
    "SchemaRegistry": {
      "Username": "<Schema Registry user name>",
      "Url": "<Schema Registry user Url>",
      "Password": "<Schema Registry password>"
    },
    "Topic": "<Topic name>"
  }
```

**Depenedency Injection**
```
services.AddQueueClient(hostContext.Configuration.GetSection("StoargeConfiguration"));

```
**Send**
- To consume Json data
```
     services.AddTransient<IErrorHandler<AnimalKey, Animal>>(_ => default);
     services.AddTransient<IPartitionsAssignedHandler<employeeKey, employeeMessage>>(_ => default);
     services.AddTransient<IHandler<Key, Value>, Worker>();
     services.AddJsonConsumer<Key, Value>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
```
- To consume Avro data
```
     services.AddTransient<IErrorHandler<AnimalKey, Animal>>(_ => default);
     services.AddTransient<IPartitionsAssignedHandler<employeeKey, employeeMessage>>(_ => default);
     services.AddTransient<IHandler<Key, Value>, Worker>();
     services.AddAvroConsumer<Key, Value>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry");
```
**Worker**
```
  public class Worker : IHandler<Key, Value>
    {
        public async Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<Key, Value>> consumeResults, CancellationToken cancellationToken)
        {
            //Processing Logic
            await Task.CompletedTask;
            return consumeResults.Select(cr => cr.TopicPartitionOffset);
           
        }
    }
```

        
