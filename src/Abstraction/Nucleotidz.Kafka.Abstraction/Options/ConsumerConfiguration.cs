using Confluent.Kafka;

namespace Nucleotidz.Kafka.Abstraction.Options
{
    public class ConsumerConfiguration : Configuration
    {
        public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;

        public int BatchSize { get; set; } = 100;

        public string GroupName { get; set; }

        public string ClientId { get; set; }

        public int TimeOut { get; set; } = 5000;

        public string Debug { get; set; }
    }
}
