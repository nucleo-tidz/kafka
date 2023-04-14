
using Confluent.Kafka;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Consumer.Json
{
    public class Worker : IHandler<AnimalKey, Animal>
    {
        public async Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<AnimalKey, Animal>> consumeResults, CancellationToken cancellationToken)
        {
            Console.WriteLine(consumeResults.Count().ToString());
            await Task.CompletedTask;
            return consumeResults.Select(cr => cr.TopicPartitionOffset);
           
        }
    }
}