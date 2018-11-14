using System;
using System.Collections.Generic;

namespace Materal.WebSocket.EventHandlers
{
    public interface IEventHandlerHelper
    {
        /// <summary>
        /// 获取所有命令处理类型
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetAllHandlerTypes();
        /// <summary>
        /// 获取命令处理类型
        /// </summary>
        /// <param name="handlerName"></param>
        /// <returns></returns>
        Type GetHandlerType(string handlerName);
    }
}
