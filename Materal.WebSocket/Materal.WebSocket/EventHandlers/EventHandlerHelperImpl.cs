using System;
using System.Collections.Generic;
using Materal.WebSocket.EventHandlers.Model;

namespace Materal.WebSocket.EventHandlers
{
    public class EventHandlerHelperImpl : IEventHandlerHelper
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        private readonly List<Type> _commandTypes;

        public EventHandlerHelperImpl(List<Type> types) => _commandTypes = types;

        public IEnumerable<Type> GetAllHandlerTypes()
        {
            return _commandTypes;
        }
        public Type GetHandlerType(string handlerName)
        {
            IEnumerable<Type> allHandler = GetAllHandlerTypes();
            foreach (Type item in allHandler)
            {
                if (item.Name.Equals(handlerName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            throw new MateralWebSocketEventHandlerException($"未找到处理器{handlerName}");
        }
    }
}
