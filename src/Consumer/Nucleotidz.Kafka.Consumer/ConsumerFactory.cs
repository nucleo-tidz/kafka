using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Nucleotidz.Kafka.Abstraction;
using Nucleotidz.Kafka.Abstraction.Options;

namespace Nucleotidz.Kafka.Consumer
{
    public class ConsumerFactory<TKey, TValue> : IConsumerFactory<TKey, TValue>
         where TKey : class
        where TValue : class
    {
        private readonly ConsumerConfiguration _consumerConfiguration;
        private readonly ISerializerFactory _serializerFactory;
        private readonly IConsumerErrorHandler<TKey, TValue> _errorHandler;
        private readonly IPartitionsAssignedHandler<TKey, TValue> _partitionsAssignedHandler;
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
        public ConsumerFactory(IOptions<ConsumerConfiguration> _consumerConfigurationOption, ISerializerFactory serializerFactory,
            IConsumerErrorHandler<TKey, TValue> errorHandler, IPartitionsAssignedHandler<TKey, TValue> partitionsAssignedHandler)
        {
            _errorHandler = errorHandler;
            _consumerConfiguration = _consumerConfigurationOption.Value;
            _serializerFactory = serializerFactory;
            _partitionsAssignedHandler = partitionsAssignedHandler;
        }

        public IConsumer<TKey, TValue> Create()
        {
            var consumerBuilder = new ConsumerBuilder<TKey, TValue>(CreateConfiguration());
            if (_errorHandler is not null)
            {
                consumerBuilder.SetErrorHandler((consumer, error) => _errorHandler.Handle(consumer, error));
            }
            if (_partitionsAssignedHandler is not null)
            {
                consumerBuilder.SetPartitionsAssignedHandler((consumer, topicPartitions) => _partitionsAssignedHandler.Handle(consumer, topicPartitions));
            }

            ArgumentNullException.ThrowIfNull(_serializerFactory, nameof(_serializerFactory));
            if (!_defaultDeserializers.Contains(typeof(TKey)))
                consumerBuilder.SetKeyDeserializer(_serializerFactory.CreateDeserializer<TKey>());
            if (!_defaultDeserializers.Contains(typeof(TValue)))
                consumerBuilder.SetValueDeserializer(_serializerFactory.CreateDeserializer<TValue>());
            return consumerBuilder.Build();
        }
        private ConsumerConfig CreateConfiguration()
        {
            return new ConsumerConfig
            {
                BootstrapServers = string.Join(",", _consumerConfiguration.BootstrapServers),
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                AutoCommitIntervalMs = 2000,
                GroupId = _consumerConfiguration.GroupName,
                ClientId = _consumerConfiguration.ClientId,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _consumerConfiguration.Username,
                SaslPassword = _consumerConfiguration.Password,
                AutoOffsetReset = _consumerConfiguration.AutoOffsetReset,
                Debug = _consumerConfiguration.Debug,
            };
        }
    }
}
