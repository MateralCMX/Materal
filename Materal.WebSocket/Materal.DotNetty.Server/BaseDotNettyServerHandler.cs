using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.WebSocket.Server.Model;

namespace Materal.DotNetty.Server
{
    public abstract class BaseDotNettyServerHandler : SimpleChannelInboundHandler<object>, IServerHandler
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            switch (msg)
            {
                case IFullHttpRequest request:
                    HandleHttpRequest(ctx, request);
                    break;
                case WebSocketFrame frame:
                    HandleWebSocketFrame(ctx, frame);
                    break;
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        protected abstract void HandleHttpRequest(IChannelHandlerContext ctx, IFullHttpRequest req);

        protected abstract void HandleWebSocketFrame(IChannelHandlerContext ctx, IByteBufferHolder frame);
    }
}
