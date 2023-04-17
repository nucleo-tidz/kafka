# Kafka


A light weight nuget package to consume and produce events from a kafka topic written on .NET 6.0
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
***Consumer***
---
**Depenedency Injection**
```
     services.AddTransient<IErrorHandler<AnimalKey, Animal>>(_ => default);
     services.AddTransient<IPartitionsAssignedHandler<employeeKey, employeeMessage>>(_ => default);
     services.AddTransient<IHandler<Key, Value>, Worker>();
     services.AddConsumer<Key, Value>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry", SerializationScheme.avro);
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

***Producer***
---
**Depenedency Injection**
```
        services.AddTransient<IConsumerErrorHandler<employeeKey, employeeMessage>, ErrorHandler>();
        services.AddTransient<IPartitionsAssignedHandler<employeeKey, employeeMessage>>(_ => default);
        services.AddTransient<IHandler<employeeKey, employeeMessage>, Worker>();
        services.AddConsumer<employeeKey, employeeMessage>(hostContext.Configuration, "Kafka", "Kafka:SchemaRegistry",SerializationScheme.avro);
```

**Worker**
```
 public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageProducer<employeeKey, employeeMessage> _messageProducer;
        public Worker(ILogger<Worker> logger, IMessageProducer<employeeKey, employeeMessage> messageProducer)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Message<employeeKey, employeeMessage> message = new Message<employeeKey, employeeMessage>();
            message.Value = new employeeMessage { Name = "Tom" };
            message.Key = new employeeKey { Id = "TM100" };
            var result = await _messageProducer.Produce(message);
            if (result.Status == PersistenceStatus.Persisted || result.Status == PersistenceStatus.PossiblyPersisted)
            {
                _logger.LogInformation("Message Prdouced");
            }
            await Task.CompletedTask;
        }
    }
```

        
