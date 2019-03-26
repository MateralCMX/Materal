using DotNetty.Transport.Channels;
using Materal.WebSocket.CommandHandlers.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.WebSocket.Commands
{
    /// <summary>
    /// 命令总线
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="ctx">Channel</param>
        /// <param name="commandData">命令数据</param>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        void Send(IChannelHandlerContext ctx, object commandData, ICommand command);
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="ctx">Channel</param>
        /// <param name="commandData">命令数据</param>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task SendAsync(IChannelHandlerContext ctx, object commandData, ICommand command);
        /// <summary>
        /// 获得所有命令处理器
        /// </summary>
        /// <returns></returns>
        List<CommandHandlerModel> GetAllCommandHandler();
    }
}
