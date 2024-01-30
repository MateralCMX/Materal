using Microsoft.Extensions.Logging;

namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBusImpl(IServiceProvider serviceProvider, ILoggerFactory? loggerFactory = null) : IEventBus
    {
        private readonly List<SubscriptionInfo> _subscriptionInfo = [];
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger? Logger = loggerFactory?.CreateLogger("EventBus");

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event"></param>
        public virtual void Publish(IEvent @event) => Task.Run(async () => await TryProcessEvent(@event));
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        public string Subscribe<T, THandler>() where T : IEvent where THandler : IEventHandler<T> => Subscribe(typeof(T), typeof(THandler));
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handlerType"></param>
        /// <exception cref="EventBusException"></exception>
        public virtual string Subscribe(Type eventType, Type handlerType)
        {
            SubscriptionInfo subscriptionInfo = GetSubscriptionInfoForEvent(eventType);
            subscriptionInfo.AddHandler(handlerType);
            Logger?.LogInformation($"订阅事件{eventType.Name},处理器为{handlerType.Name}");
            return subscriptionInfo.EventName;
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="subscriptionInfo"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        private async Task<bool> TryProcessEvent(SubscriptionInfo subscriptionInfo, IEvent @event)
        {
            try
            {
                Logger?.LogDebug($"事件触发: {subscriptionInfo.EventName}");
                if (subscriptionInfo.IsEmpty)
                {
                    Logger?.LogWarning($"未订阅事件: {subscriptionInfo.EventName}");
                    return false;
                }
                using IServiceScope serviceScope = serviceProvider.CreateScope();
                IServiceProvider service = serviceScope.ServiceProvider;
                bool result = await subscriptionInfo.HandlerEventAsync(service, Logger, @event);
                return result;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "处理事件失败");
                return false;
            }
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected async Task<bool> TryProcessEvent(IEvent @event)
        {
            try
            {
                SubscriptionInfo subscriptionInfo = GetSubscriptionInfoForEvent(@event);
                bool result = await TryProcessEvent(subscriptionInfo, @event);
                return result;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "处理事件失败");
                return false;
            }
        }
        #region 私有方法
        /// <summary>
        /// 根据Event获得订阅信息
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected SubscriptionInfo GetSubscriptionInfoForEvent(IEvent @event)
        {
            Type eventType = @event.GetType();
            return GetSubscriptionInfoForEvent(eventType);
        }
        /// <summary>
        /// 根据Event类型获得订阅信息
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        protected SubscriptionInfo GetSubscriptionInfoForEvent(Type eventType)
        {
            SubscriptionInfo? result = _subscriptionInfo.FirstOrDefault(m => m.EventType == eventType);
            if(result is null)
            {
                result = new(eventType);
                _subscriptionInfo.Add(result);
            }
            return result;
        }
        /// <summary>
        /// 根据EventName获得订阅信息
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        protected SubscriptionInfo? GetSubscriptionInfoForEvent(string eventName) => _subscriptionInfo.FirstOrDefault(m => m.EventName == eventName);
        #endregion
    }
}
