using Materal.DotNetty.Common;
using System;

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
            if (type == null) throw new MateralDotNettyException("未找到事件处理器");
            object service = _serviceProvider.GetService(type);
            if (!(service is IEventHandler eventHandler)) throw new MateralDotNettyException($"事件处理器必须实现接口{nameof(IEventHandler)}");
            return eventHandler;
        }
    }
}
