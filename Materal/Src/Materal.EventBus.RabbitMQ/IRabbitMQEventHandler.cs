namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ事件处理器
    /// </summary>
    public interface IRabbitMQEventHandler : IEventHandler
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; }
    }
}
