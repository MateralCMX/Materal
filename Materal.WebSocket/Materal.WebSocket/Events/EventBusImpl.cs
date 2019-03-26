using Materal.Common;
using Materal.WebSocket.EventHandlers;
using Materal.WebSocket.Events.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Materal.WebSocket.Events
{
    public class EventBusImpl : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventHandlerHelper _handlerHelper;
        public EventBusImpl(IServiceProvider serviceProvider, IEventHandlerHelper handlerHelper)
        {
            _serviceProvider = serviceProvider;
            _handlerHelper = handlerHelper;
        }
        public void Send(IEvent @event)
        {
            GetHandler(@event.HandlerName).Excute(@event);
        }
        public async Task SendAsync(IEvent @event)
        {
            await GetHandler(@event.HandlerName).ExcuteAsync(@event);
        }
        public List<EventHandlerModel> GetAllEventHandler()
        {
            var result = new List<EventHandlerModel>();
            IEnumerable<Type> handlerTypes = _handlerHelper.GetAllHandlerTypes();
            foreach (Type handlerType in handlerTypes)
            {
                object[] attributes = handlerType.GetCustomAttributes(typeof(DescriptionAttribute), false);
                EventHandlerModel temp;
                if (attributes.Length > 0)
                {
                    var handler = (IEventHandler)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
                    temp = new EventHandlerModel
                    {
                        Description = handler.GetDescription(),
                        HandlerName = handlerType.Name
                    };
                }
                else
                {
                    temp = new EventHandlerModel
                    {
                        Description = "",
                        HandlerName = handlerType.Name
                    };
                }
                result.Add(temp);
            }
            return result;
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <returns>处理器对象</returns>
        public IEventHandler GetHandler(string handlerName)
        {
            Type handlerType = _handlerHelper.GetHandlerType(handlerName);
            var handler = (IEventHandler)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new MateralWebSocketEventException("未找到对应处理器");
            return handler;
        }
    }
}
