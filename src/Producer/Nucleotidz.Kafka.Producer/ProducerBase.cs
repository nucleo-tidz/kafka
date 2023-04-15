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

        public virtual void Produce(Message<TKey, TValue> message, Action<DeliveryReport<TKey, TValue>> deliveryHandler = null)
        {
            _producer.Produce(_producerConfiguration.Topic, message, deliveryHandler);
            _producer.Flush();
        }
    }
}