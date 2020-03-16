using System;
using Materal.DotNetty.Client.Core;

namespace Materal.DotNetty.EventBus
{
    public class EventBusImpl : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventHandlerHelper _eventHandlerHelper;

        public EventBusImpl(EventHandlerHelper eventHandlerHelper, IServiceProvider serviceProvider)
        {
            _eventHandlerHelper = eventHandlerHelper;
            _serviceProvider = serviceProvider;
        }

        public IEventHandler GetEventHandler(string eventHandlerName)
        {
            Type type = _eventHandlerHelper.GetEventHandler(eventHandlerName);
            if (type == null) throw new DotNettyClientException("未找到事件处理器");
            object service = _serviceProvider.GetService(type);
            if (!(service is IEventHandler eventHandler)) throw new DotNettyClientException($"事件处理器必须实现接口{nameof(IEventHandler)}");
            return eventHandler;
        }
    }
}
