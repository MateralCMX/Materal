using System;
using System.Collections.Generic;
using Materal.DotNetty.Client.Core;

namespace Materal.DotNetty.EventBus
{
    public class EventHandlerHelper
    {
        /// <summary>
        /// 事件处理器类型字典
        /// </summary>
        private readonly Dictionary<string, Type> _eventHandlers = new Dictionary<string, Type>();
        /// <summary>
        /// 添加名事件处理器类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool TryAddEventHandler(Type type)
        {
            if (type == null) throw new DotNettyClientException("事件处理器类型为空");
            string key = type.Name;
            if (_eventHandlers.ContainsKey(key)) return false;
            _eventHandlers.Add(key, type);
            return true;
        }
        /// <summary>
        /// 获得事件处理器类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Type GetEventHandler(string key)
        {
            if (!_eventHandlers.ContainsKey(key)) throw new DotNettyClientException("未找到对应事件处理器");
            return _eventHandlers[key];
        }
    }
}
