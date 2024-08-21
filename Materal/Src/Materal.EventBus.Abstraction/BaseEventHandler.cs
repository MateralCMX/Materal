namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 基础事件处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEventHandler<T> : IEventHandler<T>
        where T : IEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public abstract Task HandleAsync(T @event);
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task HandleEventAsync(IEvent @event)
        {
            if (@event is not T tEvent) throw new ArgumentException("事件类型错误");
            await HandleAsync(tEvent);
        }
    }
}
