namespace Materal.TFMS.EventBus.RabbitMQ.Models
{
    /// <summary>
    /// 事件错误配置
    /// </summary>
    public class EventErrorConfig
    {
        /// <summary>
        /// 是否重试
        /// </summary>
        public bool IsRetry { get; set; } = false;
        /// <summary>
        /// 重试次数
        /// </summary>
        public uint RetryNumber { get; set; } = 5;
        /// <summary>
        /// 是否丢弃
        /// </summary>
        public bool Discard { get; set; } = true;
    }
}
