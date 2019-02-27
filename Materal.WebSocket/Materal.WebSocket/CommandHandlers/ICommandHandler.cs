using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Materal.WebSocket.Commands;

namespace Materal.WebSocket.CommandHandlers
{
    /// <summary>
    /// 命令处理器接口
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="ctx">Channel</param>
        /// <param name="frame">Frame</param>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task ExcuteAsync(IChannelHandlerContext ctx, IByteBufferHolder frame, ICommand command);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="ctx">Channel</param>
        /// <param name="frame">Frame</param>
        /// <param name="command">命令对象</param>
        void Excute(IChannelHandlerContext ctx, IByteBufferHolder frame, ICommand command);
    }
}
