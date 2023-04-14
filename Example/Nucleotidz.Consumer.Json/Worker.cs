using com.nucleotidz.rifle;
using com.nucleotidz.rifle.key;
using Confluent.Kafka;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Consumer.Json
{
    public class Worker : IHandler<EmployeeKey, Employees>
    {
        public async Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<EmployeeKey, Employees>> consumeResults, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return consumeResults.Select(cr => cr.TopicPartitionOffset);
           
        }
    }
}