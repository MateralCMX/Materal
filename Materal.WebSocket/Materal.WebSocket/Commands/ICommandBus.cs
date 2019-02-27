using DotNetty.Buffers;
using DotNetty.Transport.Channels;
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
        /// <param name="frame">Frame</param>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        void Send(IChannelHandlerContext ctx, IByteBufferHolder frame, ICommand command);
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="ctx">Channel</param>
        /// <param name="frame">Frame</param>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task SendAsync(IChannelHandlerContext ctx, IByteBufferHolder frame, ICommand command);

    }
}
