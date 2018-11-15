using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Client.Model;
using System;
using TestClient.Common;
using TestClient.Events;

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
            ConsoleHelper.TestClientWriteLine("客户端已连接");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            if(context != null) base.ChannelInactive(context);
            ConsoleHelper.TestClientWriteLine("客户端已断开连接");
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            base.ChannelRead0(ctx, msg);
            Event @event;
            switch (msg)
            {
                case TextWebSocketFrame textFrame:
                    @event = new Event("TestEventHandler")
                    {
                        Message = "测试命令",
                        ByteArrayData = null,
                        StringData = textFrame.Text()
                    };
                    ClientImpl.HandleEvent(@event);
                    //ConsoleHelper.TestClientWriteLine($"接收到{@event.StringData}");
                    break;
                case BinaryWebSocketFrame binaryFrame:
                    @event = new Event("TestEventHandler")
                    {
                        Message = "测试命令",
                        ByteArrayData = binaryFrame.Content.Array,
                        StringData = null
                    };
                    ConsoleHelper.TestClientWriteLine($"接收到Bytes[{@event.ByteArrayData.Length}]");
                    break;
                case PongWebSocketFrame _:
                    ConsoleHelper.TestClientWriteLine("接收到心跳");
                    break;
                case CloseWebSocketFrame _:
                    ConsoleHelper.TestClientWriteLine("接收到关闭");
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
