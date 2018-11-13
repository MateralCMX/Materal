using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Concurrency;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Materal.DotNetty.ClientDemo
{
    public class WebSocketClientHandler : SimpleChannelInboundHandler<object>
    {
        readonly WebSocketClientHandshaker _handshaker;
        readonly TaskCompletionSource _completionSource;

        public WebSocketClientHandler(WebSocketClientHandshaker handshaker)
        {
            _handshaker = handshaker;
            _completionSource = new TaskCompletionSource();
        }

        public Task HandshakeCompletion => _completionSource.Task;

        public override void ChannelActive(IChannelHandlerContext ctx) => _handshaker.HandshakeAsync(ctx.Channel).LinkOutcome(_completionSource);

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Console.WriteLine("WebSocket Client disconnected!");
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            IChannel ch = ctx.Channel;
            if (!_handshaker.IsHandshakeComplete)
            {
                try
                {
                    _handshaker.FinishHandshake(ch, (IFullHttpResponse)msg);
                    Console.WriteLine("WebSocket Client connected!");
                    _completionSource.TryComplete();
                }
                catch (WebSocketHandshakeException e)
                {
                    Console.WriteLine("WebSocket Client failed to connect");
                    _completionSource.TrySetException(e);
                }

                return;
            }
            switch (msg)
            {
                case IFullHttpResponse response:
                    throw new InvalidOperationException($"Unexpected FullHttpResponse (getStatus={response.Status}, content={response.Content.ToString(Encoding.UTF8)})");
                case TextWebSocketFrame textFrame:
                    Console.WriteLine($"WebSocket Client received message: {textFrame.Text()}");
                    break;
                case PongWebSocketFrame _:
                    Console.WriteLine("WebSocket Client received pong");
                    break;
                case CloseWebSocketFrame _:
                    Console.WriteLine("WebSocket Client received closing");
                    ch.CloseAsync();
                    break;
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            _completionSource.TrySetException(exception);
            ctx.CloseAsync();
        }
    }
}
