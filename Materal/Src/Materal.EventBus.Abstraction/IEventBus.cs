namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event"></param>
        Task PublishAsync(IEvent @event);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        Task SubscribeAsync<T, THandler>() where T : IEvent where THandler : IEventHandler<T>;
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        Task SubscribeAsync(Type eventType, Type eventHandlerType);
    }
}
