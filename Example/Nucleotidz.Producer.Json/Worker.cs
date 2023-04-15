using Confluent.Kafka;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Producer.Json
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageProducer<AnimalKey, Animal> _messageProducer;
        public Worker(ILogger<Worker> logger, IMessageProducer<AnimalKey, Animal> messageProducer)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Message<AnimalKey, Animal> message = new Message<AnimalKey, Animal>();
            message.Value = new Animal { category = "Omnivore",name="Human" };
            message.Key = new AnimalKey { tagid = "Ahmar" };
            var result = await _messageProducer.Produce(message);
            if (result.Status == PersistenceStatus.Persisted || result.Status == PersistenceStatus.PossiblyPersisted)
            {
                _logger.LogInformation("Message Prdouced");
            }
            await Task.CompletedTask;
        }
    }
}