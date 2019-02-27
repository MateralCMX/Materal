using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Materal.WebSocket.CommandHandlers;
using Materal.WebSocket.Commands;
using System.Threading.Tasks;
using DotNetty.Codecs.Http.WebSockets;
using Materal.ConvertHelper;
using TestWebSocket.Common;
using TestWebSocket.Events;

namespace TestWebSocket.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler
    {
        public async Task ExcuteAsync(IChannelHandlerContext ctx, IByteBufferHolder frame, ICommand command)
        {
            ConsoleHelper.TestWriteLine(command.HandlerName, "接受到命令");
            var @event = new TestEvent();
            var eventJson = new TextWebSocketFrame(@event.ToJson());
            await ctx.WriteAndFlushAsync(eventJson);
        }

        public void Excute(IChannelHandlerContext ctx, IByteBufferHolder frame, ICommand command)
        {
            ExcuteAsync(ctx, frame, command).Wait();
        }
    }
}
