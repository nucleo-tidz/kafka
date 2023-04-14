using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IPartitionsAssignedHandler<TKey,TValue>
    {
        List<TopicPartitionOffset> Handle(IConsumer<TKey, TValue> consumer, List<TopicPartition> topicPartitions);
    }
}
