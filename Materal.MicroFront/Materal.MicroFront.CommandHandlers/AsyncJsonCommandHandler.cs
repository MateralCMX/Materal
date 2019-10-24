using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using Materal.MicroFront.Commands;
using Materal.WebSocket.CommandHandlers;
using System;
using System.Threading.Tasks;

namespace Materal.MicroFront.CommandHandlers
{
    public abstract class AsyncJsonCommandHandler<T> : ICommandHandler
        where T : Command
    {
        protected T GetCommand(object commandData)
        {
            if (!(commandData is string commandJson)) throw new ArgumentException("未识别命令数据");
            var command = commandJson.JsonToObject<T>();
            return command;
        }
        public abstract Task ExcuteAsync(IChannelHandlerContext ctx, object commandData);

        public void Excute(IChannelHandlerContext ctx, object commandData)
        {
            Task.Run(async () => await ExcuteAsync(ctx, commandData));
        }
    }
}
