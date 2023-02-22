using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.Logger.CommandHandlers
{
    /// <summary>
    /// 命令处理器
    /// </summary>
    public interface ICommandHander
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="websocket"></param>
        /// <param name="command"></param>
        public void Handler(LoggerLocalServer server, WebSocketConnectionModel websocket, ICommand command);
    }
    /// <summary>
    /// 命令处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommandHander<T> : ICommandHander
        where T : ICommand
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="websocket"></param>
        /// <param name="command"></param>
        public void Handler(LoggerLocalServer server, WebSocketConnectionModel websocket, T command);
    }
}
