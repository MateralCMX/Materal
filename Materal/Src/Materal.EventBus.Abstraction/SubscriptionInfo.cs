namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    internal class SubscriptionInfo(Type eventType, Type eventHandlerType)
    {
        public Type EventType { get; } = eventType;

        public Type EventHandlerType { get; } = eventHandlerType;
    }
}
