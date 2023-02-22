using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.Logger.CommandHandlers
{
    /// <summary>
    /// 命令总线
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 订阅
        /// </summary>
        void Subscribe<TCommand, TCommandHander>()
            where TCommand : ICommand
            where TCommandHander : ICommandHander<TCommand>, new();
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="websocket"></param>
        /// <param name="message"></param>
        void Handler(LoggerLocalServer server, WebSocketConnectionModel websocket, string message);
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="websocket"></param>
        /// <param name="event"></param>
        void SendEvent(WebSocketConnectionModel websocket, IEvent @event);
    }
}
