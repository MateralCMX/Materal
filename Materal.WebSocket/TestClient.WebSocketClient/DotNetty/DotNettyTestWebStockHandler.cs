using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using Materal.DotNetty.Client.Model;
using Materal.WebSocket.Events;
using System;
using TestWebSocket.Common;
using TestWebSocket.Events;

namespace TestClient.WebSocketClient.DotNetty
{
    public class DotNettyTestWebStockHandler : DotNettyClientHandler
    {
        public DotNettyTestWebStockHandler(WebSocketClientHandshaker handshaker) : base(handshaker)
        {
        }
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);
            ConsoleHelper.TestWriteLine("客户端已连接");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            if(context != null) base.ChannelInactive(context);
            ConsoleHelper.TestWriteLine("客户端已断开连接");
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            base.ChannelRead0(ctx, msg);
            IEvent @event;
            switch (msg)
            {
                case TextWebSocketFrame textFrame:
                    @event = textFrame.Text().JsonToObject<Event>();
                    ClientImpl.HandleEvent(@event);
                    ConsoleHelper.TestWriteLine($"接收到String[{textFrame.Text()}]");
                    break;
                case BinaryWebSocketFrame binaryFrame:
                    @event = binaryFrame.GetDefaultObject<Event>();
                    ClientImpl.HandleEvent(@event);
                    ConsoleHelper.TestWriteLine($"接收到Bytes[{@event.ToJson()}]");
                    break;
                case PongWebSocketFrame _:
                    ConsoleHelper.TestWriteLine("接收到心跳");
                    break;
                case CloseWebSocketFrame _:
                    ConsoleHelper.TestWriteLine("接收到关闭");
                    ctx.Channel.CloseAsync();
                    break;
            }
        }

        public override void ChannelStart(IChannel ctx)
        {
            Console.WriteLine("正在连接服务器");
        }

        public override void OnSendMessage(object message)
        {
            Console.WriteLine(message);
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            CompletionSource.TrySetException(exception);
            ctx.CloseAsync();
        }
    }
}
