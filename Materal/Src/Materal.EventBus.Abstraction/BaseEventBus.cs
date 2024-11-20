using Materal.EventBus.RabbitMQ;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Concurrent;

namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 事件总线基类
    /// </summary>
    public abstract class BaseEventBus(IServiceProvider serviceProvider, ILogger? logger = null) : IEventBus, IAsyncDisposable
    {
        /// <summary>
        /// 订阅信息
        /// </summary>
        protected readonly Hashtable SubscriptionInfos = [];
        /// <summary>
        /// 服务提供者
        /// </summary>
        protected readonly IServiceProvider ServiceProvider = serviceProvider;
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger? Logger = logger;
        /// <inheritdoc/>
        public abstract Task PublishAsync(IEvent @event);
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="event"></param>
        protected bool HandlerEvent(string routingKey, IEvent @event)
        {
            if (SubscriptionInfos[routingKey] is not ConcurrentBag<SubscriptionInfo> eventHandlerTypes) return false;
            Parallel.ForEach(eventHandlerTypes, async m =>
            {
                try
                {
                    Logger?.LogInformation($"事件[{m.EventType.FullName}]触发,处理器为[{m.EventHandlerType.FullName}]");
                    if (ServiceProvider.GetRequiredService(m.EventHandlerType) is not IEventHandler eventHandler) return;
                    Logger?.LogDebug($"处理器[{m.EventHandlerType.FullName}]开始执行");
                    await eventHandler.HandleEventAsync(@event);
                    Logger?.LogDebug($"处理器[{m.EventHandlerType.FullName}]执行完毕");
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, $"事件[{m.EventHandlerType.FullName}]处理失败");
                }
            });
            return true;
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="eventJson"></param>
        protected bool HandlerEvent(string routingKey, string eventJson)
        {
            if (SubscriptionInfos[routingKey] is not ConcurrentBag<SubscriptionInfo> eventHandlerTypes) return false;
            Type? eventType = eventHandlerTypes.FirstOrDefault()?.EventType;
            if (eventType is null) return false;
            object eventObject = eventJson.JsonToObject(eventType);
            if (eventObject is not IEvent @event) return false;
            return HandlerEvent(routingKey, @event);
        }
        /// <inheritdoc/>
        public async Task SubscribeAsync<T, THandler>()
            where T : IEvent
            where THandler : IEventHandler<T> => await SubscribeAsync(typeof(T), typeof(THandler));
        /// <inheritdoc/>
        public abstract Task SubscribeAsync(Type eventType, Type eventHandlerType);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        protected void Subscribe(string routingKey, Type eventType, Type eventHandlerType)
        {
            if (SubscriptionInfos[routingKey] is not ConcurrentBag<SubscriptionInfo> eventHandlerTypes)
            {
                eventHandlerTypes = [];
                SubscriptionInfos[routingKey] = eventHandlerTypes;
            }
            eventHandlerTypes.Add(new(eventType, eventHandlerType));
            Logger?.LogInformation($"订阅事件{eventType.FullName},处理器为{eventHandlerType.FullName}");
        }
        /// <summary>
        /// 自动订阅
        /// </summary>
        protected async Task AutoSubscribeAsync()
        {
            foreach ((Type eventType, Type eventHandlerType) in ServiceCollectionExtensions.AutoSubscribeEvents)
            {
                await SubscribeAsync(eventType, eventHandlerType);
            }
        }
        /// <inheritdoc/>
        public virtual async ValueTask DisposeAsync()
        {
            SubscriptionInfos.Clear();
            await Task.CompletedTask;
        }
    }
}