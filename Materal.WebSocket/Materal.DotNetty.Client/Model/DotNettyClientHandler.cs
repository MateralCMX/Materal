using System;
using System.Text;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Concurrency;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;

namespace Materal.DotNetty.Client.Model
{
    public abstract class DotNettyClientHandler : SimpleChannelInboundHandler<object>
    {
        protected readonly WebSocketClientHandshaker Handshaker;
        protected readonly TaskCompletionSource CompletionSource;

        protected DotNettyClientHandler(WebSocketClientHandshaker handshaker)
        {
            Handshaker = handshaker;
            CompletionSource = new TaskCompletionSource();
        }

        public Task HandshakeCompletion => CompletionSource.Task;

        public override void ChannelActive(IChannelHandlerContext ctx) => Handshaker.HandshakeAsync(ctx.Channel).LinkOutcome(CompletionSource);

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            IChannel ch = ctx.Channel;
            if (Handshaker.IsHandshakeComplete) return;
            try
            {
                Handshaker.FinishHandshake(ch, (IFullHttpResponse)msg);
                CompletionSource.TryComplete();
            }
            catch (WebSocketHandshakeException e)
            {
                CompletionSource.TrySetException(e);
            }
        }
    }
}
