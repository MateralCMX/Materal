using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.Logger.CommandHandlers
{
    /// <summary>
    /// 基础命令处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCommandHandler<T> : ICommandHander<T>
        where T : ICommand
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="websocket"></param>
        /// <param name="command"></param>
        public abstract void Handler(LoggerLocalServer server, WebSocketConnectionModel websocket, T command);
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="websocket"></param>
        /// <param name="command"></param>
        public void Handler(LoggerLocalServer server, WebSocketConnectionModel websocket, ICommand command)
        {
            if (command is T tCommand)
            {
                Handler(server, websocket, tCommand);
            }
        }
    }
}
