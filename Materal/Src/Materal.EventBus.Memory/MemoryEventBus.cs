using Materal.EventBus.Abstraction;
using Microsoft.Extensions.Logging;

namespace Materal.EventBus.Memory
{
    /// <summary>
    /// 事件总线实现
    /// </summary>
    public class MemoryEventBus : BaseEventBus, IEventBus
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public MemoryEventBus(IServiceProvider serviceProvider, ILogger<MemoryEventBus>? logger = null) : base(serviceProvider, logger)
            => AutoSubscribeAsync().Wait();
        /// <inheritdoc/>
        public override async Task PublishAsync(IEvent @event)
        {
            Type eventType = @event.GetType();
            if (eventType.FullName is null) throw new EventBusException("获取事件名称失败");
            Logger?.LogInformation($"发布事件[{eventType.FullName}]");
            HandlerEvent(eventType.FullName, @event);
            await Task.CompletedTask;
        }
        /// <inheritdoc/>
        public override async Task SubscribeAsync(Type eventType, Type eventHandlerType)
        {
            if (eventType.FullName is null) throw new EventBusException("获取事件名称失败");
            Subscribe(eventType.FullName, eventType, eventHandlerType);
            await Task.CompletedTask;
        }
    }
}
