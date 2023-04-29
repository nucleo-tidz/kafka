using Confluent.Kafka;
using Customer.Orders;
using Nucleotidz.Kafka.Abstraction;

namespace Nucleotidz.Consumer.Avro
{
    public class ErrorHandler : IConsumerErrorHandler<CustomerOrderKey, CustomerOrder>
    {
        public void Handle(IConsumer<CustomerOrderKey, CustomerOrder> consumer, Error error)
        {
            Console.WriteLine(error.ToString());
        }
    }
}
