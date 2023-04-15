using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Producer
{
    public class Producer<TKey, TValue> : ProducerBase<TKey, TValue>, IMessageProducer<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        public Producer(IProducerFactory<TKey, TValue> producerFactory, IOptions<ProducerConfiguration> producerConfigurationOption)
            : base(producerFactory, producerConfigurationOption)
        { }
        public override void Produce(Message<TKey, TValue> message, Action<DeliveryReport<TKey, TValue>> deliveryHandler = null)
        {
            base.Produce(message, deliveryHandler);
        }
    }
}
