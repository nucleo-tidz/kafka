using Confluent.Kafka;
using Nucleotidz.Kafka.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Consumer.Json
{
    internal class PartitionsAssignedHandler : IPartitionsAssignedHandler<AnimalKey, Animal>
    {
        public List<TopicPartitionOffset> Handle(IConsumer<AnimalKey, Animal> consumer, List<TopicPartition> topicPartitions)
        {
            return topicPartitions.Select(p => new TopicPartitionOffset(p.Topic, p.Partition, Offset.Beginning)).ToList();
        }
    }
}
