namespace Nucleotidz.Kafka.Abstraction.Options
{
    public class SchemaRegistryConfiguration
    {
        /// <summary>
        /// Gets or sets schema registry URL.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets schema registry user name.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets schema registry password.
        /// </summary>
        public string? Password { get; set; }
    }
}