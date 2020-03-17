using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.EventBus
{
    public interface IEventHandler
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Type EventType { get; }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="@event"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        Task HandlerAsync(IEvent @event, IChannel channel);
    }
}
