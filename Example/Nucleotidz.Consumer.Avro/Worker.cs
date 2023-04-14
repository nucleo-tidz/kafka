namespace Nucleotidz.Consumer.Avro
{
    using com.nucleotidz.employee;
    using com.nucleotidz.employee.key;
    using Confluent.Kafka;
    using Nucleotidz.Kafka.Abstraction;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : IHandler<employeeKey, employeeMessage>
    {
        public async Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<employeeKey, employeeMessage>> consumeResults, CancellationToken cancellationToken)
        {
            Console.WriteLine(consumeResults.Count().ToString());
            return consumeResults.Select(cr => cr.TopicPartitionOffset);           
        }
    }
}