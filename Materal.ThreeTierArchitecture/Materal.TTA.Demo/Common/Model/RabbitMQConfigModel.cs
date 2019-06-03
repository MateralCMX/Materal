namespace Common.Model
{
    /// <summary>
    /// 消息中间件配置模型
    /// </summary>
    public class RabbitMQConfigModel
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Exchang名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// Queues名称
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 链接数量
        /// </summary>
        public int ConnectionNumber { get; set; }
        /// <summary>
        /// 通道数量
        /// </summary>
        public int ChannelNumber { get; set; }
    }
}
