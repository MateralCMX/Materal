using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Materal.RabbitMQHelper
{
    public sealed class ConsumingService : BaseService
    {
        private readonly ConsumingConfig _config;
        public ConsumingService(ConsumingConfig config) : base(config)
        {
            _config = config;
        }

        private EventingBasicConsumer GetConsumer(IModel channel)
        {
            channel.BasicQos(0, _config.MaxMessageCount, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(_config.QueueConfig.QueueName, _config.AutoAck, consumer);
            return consumer;
        }
        public void RunServer(ConsumingRunConfig consumingRunConfig)
        {
            for (var i = 0; i < _config.ChannelNumber; i++)
            {
                IModel channel = i == 0 ? GetChannel(_connection, _config.ExchangeConfig, _config.QueueConfig) : GetChannel(_connection, _config.QueueConfig);
                EventingBasicConsumer consumer = GetConsumer(channel);
                consumer.Registered += (sender, e) => consumingRunConfig.Registered?.Invoke(channel, sender, e, _config);
                consumer.Received += (sender, e) => consumingRunConfig.Received?.Invoke(channel, e.Body, sender, e, _config);
                consumer.Unregistered += (sender, e) => consumingRunConfig.Unregistered?.Invoke(channel, sender, e, _config);
                consumer.Shutdown += (sender, e) => consumingRunConfig.Shutdown?.Invoke(channel, sender, e, _config);
            }
        }
        public void RunServerAsync(ConsumingRunAsyncConfig consumingRunConfig)
        {
            for (var i = 0; i < _config.ChannelNumber; i++)
            {
                IModel channel = i == 0 ? GetChannel(_connection, _config.ExchangeConfig, _config.QueueConfig) : GetChannel(_connection, _config.QueueConfig);
                EventingBasicConsumer consumer = GetConsumer(channel);
                consumer.Registered += async (sender, e) =>
                {
                    if (consumingRunConfig.RegisteredAsync == null) return;
                    await consumingRunConfig.RegisteredAsync(channel, sender, e, _config);
                };
                consumer.Received += async (sender, e) =>
                {
                    if (consumingRunConfig.ReceivedAsync == null) return;
                    await consumingRunConfig.ReceivedAsync(channel, e.Body, sender, e, _config);
                };
                consumer.Unregistered += async (sender, e) =>
                {
                    if (consumingRunConfig.UnregisteredAsync == null) return;
                    await consumingRunConfig.UnregisteredAsync(channel, sender, e, _config);
                };
                consumer.Shutdown += async (sender, e) =>
                {
                    if (consumingRunConfig.ShutdownAsync == null) return;
                    await consumingRunConfig.ShutdownAsync(channel, sender, e, _config);
                };
            }
        }
    }
}
