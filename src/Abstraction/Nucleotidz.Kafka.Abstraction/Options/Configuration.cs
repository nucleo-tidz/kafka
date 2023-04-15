using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction.Options
{
    public class Configuration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> BootstrapServers { get; set; }
        public string Topic { get; set; }
    }
}
