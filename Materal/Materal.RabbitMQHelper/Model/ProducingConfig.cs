using System.Collections.Generic;
using System.Text;

namespace Materal.RabbitMQHelper.Model
{
    /// <summary>
    /// 简单发布者配置
    /// </summary>
    public class ProducingConfig: ServiceConfig
    {
        /// <summary>
        /// 交换机配置
        /// </summary>
        public ExchangeConfig ExchangeConfig { get; set; }
        /// <summary>
        /// 队列配置
        /// </summary>
        public IList<QueueConfig> QueueConfigs { get; set; }
    }
}
