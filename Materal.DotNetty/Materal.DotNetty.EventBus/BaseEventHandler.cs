using Materal.DotNetty.Common;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Materal.DotNetty.EventBus
{
    public abstract class BaseEventHandler<T> : IEventHandler
    {
        public Type EventType => typeof(T);
        public async Task HandlerAsync(IEvent @event, ClientWebSocket channel)
        {
            T trueEvent = GetEvent(@event);
            await HandlerAsync(trueEvent, channel);
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="event"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract Task HandlerAsync(T @event, ClientWebSocket channel);
        /// <summary>
        /// 获得命令对象
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public T GetEvent(IEvent @event)
        {
            if (@event is T trueEvent) return trueEvent;
            throw new DotNettyException("命令未识别");
        }
    }
}
