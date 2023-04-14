using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleotidz.Kafka.Abstraction.Options
{
    public class ConsumerConfiguration
    {
       
        public string Username { get; set; }


        public string Password { get; set; }

       
        public IEnumerable<string> BootstrapServers { get; set; }

  
        public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;

        public string Topic { get; set; }
        public int BatchSize { get; set; } = 100;

        public string GroupName { get; set; }

       
        public string ClientId { get; set; }



    }
}
