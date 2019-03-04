using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Materal.RabbitMQHelper
{
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
                    string message = clientConfig.Encoding.GetString(e.Body);
                    if (clientConfig.ReceivedAsync != null)
                    {
                        await clientConfig.ReceivedAsync(channel, message, sender, e);
                    }
                    else
                    {
                        clientConfig.Received?.Invoke(channel, message, sender, e);
                    }
                };
            })
        {
            _clientMQConfig = clientConfig;
        }
    }
}
