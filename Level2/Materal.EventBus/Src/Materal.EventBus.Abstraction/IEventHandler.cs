namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleEventAsync(IEvent @event);
    }
    /// <summary>
    /// 事件处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler<T> : IEventHandler
        where T : IEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync(T @event);
    }
}
