using Microsoft.Extensions.Logging;

namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class SubscriptionInfo(Type eventType)
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; } = eventType.Name;
        /// <summary>
        /// 事件类型
        /// </summary>
        public Type EventType { get; } = eventType;
        /// <summary>
        /// 处理器类型
        /// </summary>
        private List<Type> _handlerTypes { get; } = [];
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _handlerTypes.Count == 0;
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handlerType"></param>
        public void AddHandler(Type handlerType)
        {
            if (_handlerTypes.Contains(handlerType)) return;
            _handlerTypes.Add(handlerType);
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task<bool> HandlerEventAsync(IServiceProvider serviceProvider, ILogger? logger, IEvent @event)
        {
            bool result = true;
            foreach (Type handlerType in _handlerTypes)
            {
                result = await HandlerSubscriptionsAsync(serviceProvider, logger, handlerType, @event) && result;
            }
            return result;
        }

        /// <summary>
        /// 处理订阅
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="handlerType"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        private async Task<bool> HandlerSubscriptionsAsync(IServiceProvider serviceProvider, ILogger? logger, Type handlerType, IEvent @event)
        {
            Type eventType = @event.GetType();
            object? handler = serviceProvider.GetService(handlerType);
            MethodInfo? methodInfo = handlerType.GetMethod(nameof(IEventHandler<IEvent>.HandlerAsync), [eventType]);
            if (methodInfo is null) return false;
            logger?.LogDebug($"处理器{handlerType.Name}开始执行");
            object? handlerResult = methodInfo.Invoke(handler, new[] { @event });
            logger?.LogDebug($"处理器{handlerType.Name}执行完毕");
            if (handlerResult is Task<bool> task) return await task;
            return true;
        }
    }
}
