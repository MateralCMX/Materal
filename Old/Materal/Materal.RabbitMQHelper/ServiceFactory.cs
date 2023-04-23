using Materal.RabbitMQHelper.Model;
using System.Text;

namespace Materal.RabbitMQHelper
{
    public class ServiceFactory
    {
        private readonly ServiceConfig _config;
        public ServiceFactory()
        {
            _config = new ServiceConfig();
        }
        public ServiceFactory(ServiceConfig config)
        {
            _config = config;
        }
        /// <summary>
        /// 获得简单生产者服务
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="queueName"></param>
        /// <param name="Durable"></param>
        /// <returns></returns>
        public ProducingService GetSimplestProducingService(string exchangeName, string queueName, bool Durable = false)
        {
            var config = new ProducingConfig
            {
                Encoding = Encoding.UTF8,
                ExchangeConfig = new ExchangeConfig { ExchangeName = exchangeName },
                HostName = _config.HostName,
                Port = _config.Port,
                UserName = _config.UserName,
                Password = _config.Password,
                QueueConfigs = new[]
                {
                    new QueueConfig
                    {
                        Durable = Durable,
                        QueueName = queueName,
                    }
                },
                Timeout = _config.Timeout
            };
            return new ProducingService(config);
        }
        /// <summary>
        /// 获得简单消费者服务
        /// </summary>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="channelNumber">通道数量</param>
        /// <param name="Durable">持久化</param>
        /// <returns></returns>
        public ConsumingService GetSimplestConsumingService(string exchangeName, string queueName, int channelNumber = 1 , bool Durable = false)
        {
            var config = new ConsumingConfig
            {
                Encoding = Encoding.UTF8,
                ExchangeConfig = new ExchangeConfig { ExchangeName = exchangeName },
                HostName = _config.HostName,
                Port = _config.Port,
                UserName = _config.UserName,
                Password = _config.Password,
                QueueConfig = new QueueConfig
                {
                    Durable = Durable,
                    QueueName = queueName,
                },
                Timeout = _config.Timeout,
                ChannelNumber = channelNumber,
                MaxMessageCount = 1
            };
            return new ConsumingService(config);
        }
    }
}
