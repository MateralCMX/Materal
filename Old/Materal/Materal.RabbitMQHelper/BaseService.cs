using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;

namespace Materal.RabbitMQHelper
{
    public abstract class BaseService
    {
        protected IConnection _connection;
        protected BaseService(IServiceConfig config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.HostName,
                Port = config.Port,
                UserName = config.UserName,
                Password = config.Password,
                RequestedConnectionTimeout = config.Timeout,
                SocketReadTimeout = config.Timeout,
                SocketWriteTimeout = config.Timeout
            };
            _connection = factory.CreateConnection();
        }
        /// <summary>
        /// 获得一个通道
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="exchangeConfig">交换机配置</param>
        /// <param name="queueConfig">队列配置</param>
        /// <returns></returns>
        public virtual IModel GetChannel(IConnection connection, ExchangeConfig exchangeConfig, QueueConfig queueConfig)
        {
            IModel channel = GetChannel(connection, queueConfig);
            channel.ExchangeDeclare(exchangeConfig.ExchangeName, exchangeConfig.ExchangeCategoryString);
            channel.QueueDeclare(queueConfig.QueueName, queueConfig.Durable, queueConfig.Exclusive, queueConfig.AutoDelete, queueConfig.Arguments);
            channel.QueueBind(queueConfig.QueueName, exchangeConfig.ExchangeName, queueConfig.RoutingKey);
            return channel;
        }
        /// <summary>
        /// 获得一个通道
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="queueConfig">队列配置</param>
        /// <returns></returns>
        public virtual IModel GetChannel(IConnection connection, QueueConfig queueConfig)
        {
            IModel channel = connection.CreateModel();
            if (queueConfig.Durable)
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
            }
            return channel;
        }
    }
}
