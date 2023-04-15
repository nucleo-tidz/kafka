using com.nucleotidz.employee;
using com.nucleotidz.employee.key;
using Confluent.Kafka;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Producer.Avro
{
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
}