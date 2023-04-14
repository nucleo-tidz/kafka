using Confluent.Kafka;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Kafka.Abstraction
{
    public interface IHandler<TKey, TValue>
    {
        Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<TKey, TValue>> consumeResults, CancellationToken cancellationToken);
    }
}
