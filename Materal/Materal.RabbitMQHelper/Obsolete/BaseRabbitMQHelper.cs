using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace Materal.RabbitMQHelper
{
    [Obsolete("该帮助类已过时，请使用最新的RabbitMQService")]
    public abstract class BaseRabbitMQHelper : IDisposable
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        private IRabbitMQConfig MQConfig { get; }

        /// <summary>
        /// 链接组
        /// </summary>
        protected Dictionary<IConnection, List<IModel>> Connections { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <param name="connectionNumber">链接数量</param>
        /// <param name="channelNumber">通道数量</param>
        /// <param name="channelAction">通道处理</param>
        protected BaseRabbitMQHelper(IRabbitMQConfig config, int connectionNumber, int channelNumber, Action<IModel> channelAction = null)
        {
            MQConfig = config;
            Connections = new Dictionary<IConnection, List<IModel>>();
            var factory = new ConnectionFactory { HostName = MQConfig.HostName };
            for (int i = 0; i < connectionNumber; i++)
            {
                IConnection connection = factory.CreateConnection();
                var channels = new List<IModel>();
                for (int j = 0; j < channelNumber; j++)
                {
                    IModel channel = connection.CreateModel();
                    if (MQConfig.Durable)
                    {
                        IBasicProperties properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                    }
                    if (j == 0)
                    {
                        channel.ExchangeDeclare(MQConfig.ExchangeName, MQConfig.ExchangeCategoryString, MQConfig.Durable, MQConfig.AutoDelete, null);
                        channel.QueueDeclare(MQConfig.QueueName, MQConfig.Durable, MQConfig.Exclusive, MQConfig.AutoDelete, null);
                        channel.QueueBind(MQConfig.QueueName, MQConfig.ExchangeName, config.RoutingKey);
                    }
                    channelAction?.Invoke(channel);
                    channels.Add(channel);
                }
                Connections[connection] = channels;
            }
        }
        public void Dispose()
        {
            foreach (KeyValuePair<IConnection, List<IModel>> connection in Connections)
            {
                foreach (IModel channel in connection.Value)
                {
                    channel.Dispose();
                }
                connection.Key.Dispose();
            }
        }
    }
}
