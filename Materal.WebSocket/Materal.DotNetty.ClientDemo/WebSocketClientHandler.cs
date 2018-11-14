using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Client.Model;
using System;

namespace Materal.DotNetty.ClientDemo
{
    public class WebSocketClientHandler : DotNettyClientHandler
    {

        public WebSocketClientHandler(WebSocketClientHandshaker handshaker) : base(handshaker)
        {
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Console.WriteLine("客户端已断开连接");
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            base.ChannelRead0(ctx, msg);
            switch (msg)
            {
                //case IFullHttpResponse response:
                    //throw new InvalidOperationException($"Unexpected FullHttpResponse (getStatus={response.Status}, content={response.Content.ToString(Encoding.UTF8)})");
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

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            CompletionSource.TrySetException(exception);
            ctx.CloseAsync();
        }
    }
}
