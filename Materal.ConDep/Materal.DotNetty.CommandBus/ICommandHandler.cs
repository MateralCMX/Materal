using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.CommandBus
{
    public interface ICommandHandler
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        Type CommandType { get; }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="command"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        Task HandlerAsync(ICommand command, IChannel channel);
    }
}
