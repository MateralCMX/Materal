using DotNetty.Transport.Channels;
using System.Threading.Tasks;

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
        /// <param name="commandData">命令数据</param>
        /// <returns></returns>
        Task ExcuteAsync(IChannelHandlerContext ctx, object commandData);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="ctx">Channel</param>
        /// <param name="commandData">命令数据</param>
        void Excute(IChannelHandlerContext ctx, object commandData);
    }
}
