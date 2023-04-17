using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Producer
{
    public abstract class ProducerBase<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private readonly Confluent.Kafka.IProducer<TKey, TValue> _producer;
        private readonly ProducerConfiguration _producerConfiguration;
        public ProducerBase(IProducerFactory<TKey, TValue> producerFactory, IOptions<ProducerConfiguration> producerConfigurationOption)
        {
            _producer = producerFactory.Create();
            _producerConfiguration = producerConfigurationOption.Value;
        }

        public virtual async Task<DeliveryResult<TKey, TValue>> Produce(Message<TKey, TValue> message)
        {
            var deilvryResult= await _producer.ProduceAsync(_producerConfiguration.Topic,message);
            _producer.Flush();
            return deilvryResult;
        }
    }
}