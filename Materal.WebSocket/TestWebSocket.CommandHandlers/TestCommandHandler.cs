using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using Materal.WebSocket.CommandHandlers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TestWebSocket.Common;
using TestWebSocket.Events;

namespace TestWebSocket.CommandHandlers
{
    [Description("测试命令")]
    public class TestCommandHandler : ICommandHandler
    {
        public async Task ExcuteAsync(IChannelHandlerContext ctx, object commandData)
        {
            if (!(commandData is string commandJson)) throw new ApplicationException("命令数据错误");
            ConsoleHelper.TestWriteLine(commandJson, "接受到命令");
            var @event = new TestEvent
            {
                StringData = "服务器返回"
            };
            var eventJson = new TextWebSocketFrame(@event.ToJson());
            await ctx.WriteAndFlushAsync(eventJson);
        }

        public void Excute(IChannelHandlerContext ctx, object commandData)
        {
            ExcuteAsync(ctx, commandData).Wait();
        }
    }
}
