using System.Threading.Tasks;
using Materal.WebSocket.Events;

namespace Materal.WebSocket.EventHandlers
{
    /// <summary>
    /// 命令处理器接口
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="event">事件对象</param>
        /// <returns></returns>
        Task ExcuteAsync(IEvent @event);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="event">事件对象</param>
        void Excute(IEvent @event);
    }
}
