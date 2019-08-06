using System;
using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Materal.RabbitMQHelper
{
    [Obsolete("该帮助类已过时，请使用最新的IRabbitMQConsumingService")]
    public class RabbitMQClientHelper : BaseRabbitMQHelper
    {
        /// <summary>
        /// 客户端MQ配置
        /// </summary>
        private readonly RabbitMQClientConfig _clientMQConfig;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="clientConfig">客户端配置</param>
        /// <param name="connectionNumber">链接数量</param>
        /// <param name="channelNumber">通道数量</param>
        public RabbitMQClientHelper(RabbitMQClientConfig clientConfig, int connectionNumber, int channelNumber) : base(clientConfig, connectionNumber, channelNumber,
            channel =>
            {
                channel.BasicQos(0, clientConfig.MaxMessageCount, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(clientConfig.QueueName, clientConfig.AutoAck, consumer);
                consumer.Received += async (sender, e) =>
                {
                    if (clientConfig.ReceivedAsync != null)
                    {
                        await clientConfig.ReceivedAsync(channel, e.Body, sender, e, clientConfig);
                    }
                    clientConfig.Received?.Invoke(channel, e.Body, sender, e, clientConfig);
                };
            })
        {
            _clientMQConfig = clientConfig;
        }
    }
}
