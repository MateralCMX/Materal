using Materal.WebSocket.EventHandlers;
using Materal.WebSocket.Events.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Materal.WebSocket.Events
{
    public class EventBusImpl : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventHandlerHelper _eventHandlerHelper;
        public EventBusImpl(IServiceProvider serviceProvider, IEventHandlerHelper eventHandlerHelper)
        {
            _serviceProvider = serviceProvider;
            _eventHandlerHelper = eventHandlerHelper;
        }
        public void Send(IEvent @event)
        {
            GetHandler(@event.HandlerName).Excute(@event);
        }
        public async Task SendAsync(IEvent @event)
        {
            await GetHandler(@event.HandlerName).ExcuteAsync(@event);
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <returns>处理器对象</returns>
        public IEventHandler GetHandler(string handlerName)
        {
            Type handlerType = _eventHandlerHelper.GetHandlerType(handlerName);
            var handler = (IEventHandler)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new EventException("未找到对应处理器");
            return handler;
        }
    }
}
