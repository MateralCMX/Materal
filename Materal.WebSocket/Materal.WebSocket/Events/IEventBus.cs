using Materal.WebSocket.Events.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.WebSocket.Events
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="event">事件对象</param>
        void Send(IEvent @event);

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="event">事件对象</param>
        /// <returns></returns>
        Task SendAsync(IEvent @event);
        /// <summary>
        /// 获得所有时间处理器
        /// </summary>
        /// <returns></returns>
        List<EventHandlerModel> GetAllEventHandler();
    }
}
