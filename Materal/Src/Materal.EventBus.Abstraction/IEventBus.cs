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
        void Publish(IEvent @event);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        string Subscribe<T, THandler>() where T : IEvent where THandler : IEventHandler<T>;
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        string Subscribe(Type eventType, Type eventHandlerType);
    }
}
