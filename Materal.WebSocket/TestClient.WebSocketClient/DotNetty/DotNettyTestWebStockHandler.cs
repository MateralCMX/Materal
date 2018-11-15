using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Client.Model;
using System;

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
            Console.WriteLine("客户端已连接");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            if(context != null) base.ChannelInactive(context);
            Console.WriteLine("客户端已断开连接");
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            base.ChannelRead0(ctx, msg);
            switch (msg)
            {
                case TextWebSocketFrame textFrame:
                    Console.WriteLine($"WebSocket Client received message: {textFrame.Text()}");
                    break;
                case PongWebSocketFrame _:
                    Console.WriteLine("WebSocket Client received pong");
                    break;
                case CloseWebSocketFrame _:
                    Console.WriteLine("WebSocket Client received closing");
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
