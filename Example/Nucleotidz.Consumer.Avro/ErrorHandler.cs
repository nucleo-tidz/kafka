using com.nucleotidz.employee.key;
using com.nucleotidz.employee;
using Newtonsoft.Json.Linq;
using Nucleotidz.Kafka.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Nucleotidz.Consumer.Avro
{
    public class ErrorHandler : IErrorHandler<employeeKey, employeeMessage>
    {
        public void Handle(IConsumer<employeeKey, employeeMessage> consumer, Error error)
        {
            Console.WriteLine(error.ToString());
        }
    }
}
