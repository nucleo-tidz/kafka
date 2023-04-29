
using Confluent.Kafka;
using Customer.Orders;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Producer.Avro
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageProducer<CustomerOrderKey, CustomerOrder> _messageProducer;
        public Worker(ILogger<Worker> logger, IMessageProducer<CustomerOrderKey, CustomerOrder> messageProducer)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (int i = 0; i < 5; i++)
            {
                Message<CustomerOrderKey, CustomerOrder> message = new Message<CustomerOrderKey, CustomerOrder>();
                message.Value = new CustomerOrder { status = "L", orderNumber = $"OD{i.ToString()}", orderItems = new List<Items> { new Items { Name = "Car", quantity = 1 } } };
                message.Key = new CustomerOrderKey { orderNumber = $"OD{i.ToString()}" };
                var result = await _messageProducer.Produce(message);
                if (result.Status == PersistenceStatus.Persisted || result.Status == PersistenceStatus.PossiblyPersisted)
                {
                    _logger.LogInformation("Message Prdouced");
                }
                await Task.CompletedTask;
            }
        }
    }
}