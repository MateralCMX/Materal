using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using Materal.MicroFront.Commands;
using Materal.WebSocket.CommandHandlers;

namespace Materal.MicroFront.CommandHandlers
{
    public abstract class JsonCommandHandler<T> : ICommandHandler
        where T : Command
    {
        protected T GetCommand(object commandData)
        {
            if (!(commandData is string commandJson)) throw new ArgumentException("未识别命令数据");
            var command = commandJson.JsonToObject<T>();
            return command;
        }

        public async Task ExcuteAsync(IChannelHandlerContext ctx, object commandData)
        {
            await Task.Run(() => Excute(ctx, commandData));
        }

        public abstract void Excute(IChannelHandlerContext ctx, object commandData);
    }
}
