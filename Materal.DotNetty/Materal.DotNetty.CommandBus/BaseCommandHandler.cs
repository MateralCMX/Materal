using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.CommandBus
{
    public abstract class BaseCommandHandler<T> : ICommandHandler where T : ICommand
    {
        public Type CommandType => typeof(T);
        public virtual async Task HandlerAsync(ICommand command, IChannel channel)
        {
            T trueCommand = GetCommand(command);
            await HandlerAsync(trueCommand, channel);
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="command"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract Task HandlerAsync(T command, IChannel channel);
        /// <summary>
        /// 获得命令对象
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public T GetCommand(ICommand command)
        {
            if (command is T trueCommand) return trueCommand;
            throw new DotNettyException("命令未识别");
        }
    }
}
