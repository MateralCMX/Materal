using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;

namespace Materal.RabbitMQHelper
{
    public sealed class ProducingService : BaseService
    {
        private readonly ProducingConfig _config;
        private readonly Dictionary<QueueConfig, IModel> _channels;
        public ProducingService(ProducingConfig config) : base(config)
        {
            _config = config;
            _channels = new Dictionary<QueueConfig, IModel>();
            var isFirst = true;
            if (_config.QueueConfigs == null || _config.QueueConfigs.Count <= 0) return;
            foreach (QueueConfig queueConfig in _config.QueueConfigs)
            {
                if (_channels.ContainsKey(queueConfig)) continue;
                IModel channel;
                if (isFirst)
                {
                    if (_config.ExchangeConfig == null) continue;
                    channel = GetChannel(_connection, _config.ExchangeConfig, queueConfig);
                    isFirst = false;
                }
                else
                {
                    channel = GetChannel(_connection, queueConfig);
                }
                _channels.Add(queueConfig, channel);
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            byte[] body = _config.Encoding.GetBytes(message);
            SendBytes(body);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="body"></param>
        public void SendBytes(byte[] body)
        {
            Parallel.ForEach(_channels, channel =>
            {
                if (_config.ExchangeConfig == null) return;
                channel.Value.BasicPublish(_config.ExchangeConfig.ExchangeName, channel.Key.RoutingKey, null, body);
            });
        }
    }
}
