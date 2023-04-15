using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Producer
{
    internal class ProducerFactory<TKey, TValue> : IProducerFactory<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private readonly ProducerConfiguration _producerConfiguration;
        private readonly ISerializerFactory _serializerFactory;
        private readonly IProducerErrorHandler<TKey, TValue> _errorHandler;

        private readonly HashSet<Type> _defaultDeserializers = new()
    {
        typeof(Null),
        typeof(Ignore),
        typeof(int),
        typeof(long),
        typeof(string),
        typeof(float),
        typeof(double),
        typeof(byte[]),
    };
        public ProducerFactory(IOptions<ProducerConfiguration> producerConfigurationOption, ISerializerFactory serializerFactory,
            IProducerErrorHandler<TKey, TValue> errorHandler)
        {
            ArgumentNullException.ThrowIfNull(producerConfigurationOption, nameof(producerConfigurationOption));
            ArgumentNullException.ThrowIfNull(serializerFactory, nameof(serializerFactory));
            _producerConfiguration = producerConfigurationOption.Value;
            _serializerFactory = serializerFactory;
            _errorHandler = errorHandler;
        }
        public IProducer<TKey, TValue> Create()
        {
            var producerBuilder = new ProducerBuilder<TKey, TValue>(CreateConfiguration());
            producerBuilder.SetKeySerializer(_serializerFactory.CreateSerializer<TKey>());
            producerBuilder.SetValueSerializer(_serializerFactory.CreateSerializer<TValue>());
            if (_errorHandler is not null)
            {
                producerBuilder.SetErrorHandler((proucer, error) => _errorHandler.Handle(proucer, error));
            }

            return producerBuilder.Build();
        }
        private ProducerConfig CreateConfiguration()
        {
            return new ProducerConfig
            {
                BootstrapServers = string.Join(",", _producerConfiguration.BootstrapServers),
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _producerConfiguration.Username,
                SaslPassword = _producerConfiguration.Password,
            };
        }
    }
}
