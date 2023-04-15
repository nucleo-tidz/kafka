using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IMessageProducer<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        void Produce(Message<TKey, TValue> message, Action<DeliveryReport<TKey, TValue>> deliveryHandler = null);
    }
}
