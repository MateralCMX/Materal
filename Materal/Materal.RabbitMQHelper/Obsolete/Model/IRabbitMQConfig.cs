using System.Text;

namespace Materal.RabbitMQHelper.Model
{
    public interface IRabbitMQConfig
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        string HostName { get; set; }
        /// <summary>
        /// 交换机名称
        /// </summary>
        string ExchangeName { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        ExchangeCategoryEnum ExchangeCategory { get; set; }
        /// <summary>
        /// 交换机类型文本
        /// </summary>
        string ExchangeCategoryString { get; }
        /// <summary>
        /// Queues名称
        /// </summary>
        string QueueName { get; set; }
        /// <summary>
        /// 持久化
        /// </summary>
        bool Durable { get; set; }
        /// <summary>
        /// 排外
        /// </summary>
        bool Exclusive { get; set; }
        /// <summary>
        /// 自动删除
        /// </summary>
        bool AutoDelete { get; set; }
        /// <summary>
        /// 路游键
        /// </summary>
        string RoutingKey { get; set; }
        /// <summary>
        /// 字符集
        /// </summary>
        Encoding Encoding { get; set; }
    }
}
