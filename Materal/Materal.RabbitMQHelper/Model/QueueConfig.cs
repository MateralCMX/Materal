using System.Collections.Generic;

namespace Materal.RabbitMQHelper.Model
{
    /// <summary>
    /// 队列配置
    /// </summary>
    public class QueueConfig
    {
        /// <summary>
        /// Queues名称
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 排外
        /// </summary>
        public bool Exclusive { get; set; } = false;
        /// <summary>
        /// 持久化
        /// </summary>
        public bool Durable { get; set; } = false;
        /// <summary>
        /// 自动删除
        /// </summary>
        public bool AutoDelete { get; set; } = false;
        /// <summary>
        /// 参数组
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; } = null;
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey { get; set; } = string.Empty;
    }
}
