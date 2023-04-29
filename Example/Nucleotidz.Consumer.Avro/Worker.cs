namespace Nucleotidz.Consumer.Avro
{

    using Confluent.Kafka;
    using Customer.Orders;
    using Nucleotidz.Kafka.Abstraction;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : IHandler<CustomerOrderKey, CustomerOrder>
    {
        public async Task<IEnumerable<TopicPartitionOffset>> HandleAsync(IEnumerable<ConsumeResult<CustomerOrderKey, CustomerOrder>> consumeResults, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Console.WriteLine(consumeResults.Count().ToString());
            return consumeResults.Select(cr => cr.TopicPartitionOffset);           
        }
    }
}