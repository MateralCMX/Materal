namespace Materal.RabbitMQHelper.Model
{
    public class ConsumingConfig : ServiceConfig
    {
        /// <summary>
        /// 最大消息数量
        /// </summary>
        public ushort MaxMessageCount { get; set; } = 1;
        /// <summary>
        /// 自动提交
        /// </summary>
        public bool AutoAck { get; set; } = false;
        /// <summary>
        /// 通道数量
        /// </summary>
        public int ChannelNumber { get; set; } = 1;
        /// <summary>
        /// 交换机配置
        /// </summary>
        public ExchangeConfig ExchangeConfig { get; set; }
        /// <summary>
        /// 队列配置
        /// </summary>
        public QueueConfig QueueConfig { get; set; }
    }
}
