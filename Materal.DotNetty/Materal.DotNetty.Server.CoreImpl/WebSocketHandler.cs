using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using Materal.DotNetty.CommandBus;
using Materal.DotNetty.Server.Core;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class WebSocketHandler : ServerHandlerContext
    {
        private const string WebSocketUrl = "/websocket";
        private WebSocketServerHandshaker _handShaker;
        private readonly ICommandBus _commandBus;

        public WebSocketHandler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public override async Task HandlerAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder)
        {
            if (byteBufferHolder is IFullHttpRequest request && request.Uri.Equals(WebSocketUrl, StringComparison.OrdinalIgnoreCase))
            {
                await ProtocolUpdateAsync(ctx, request);
            }
            else
            {
                await HandlerAsync<WebSocketFrame>(ctx, byteBufferHolder, HandlerRequestAsync);
            }
        }
        #region 私有方法
        /// <summary>
        /// 获取WebSocket地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual string GetWebSocketAddress(IFullHttpRequest request)
        {
            request.Headers.TryGet(HttpHeaderNames.Host, out ICharSequence value);
            string address = "ws://" + value.ToString() + WebSocketUrl;
            return address;
        }
        /// <summary>
        /// 协议升级
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="request"></param>
        protected virtual async Task ProtocolUpdateAsync(IChannelHandlerContext ctx, IFullHttpRequest request)
        {
            string address = GetWebSocketAddress(request);
            var webSocketServerHandshakerFactory = new WebSocketServerHandshakerFactory(address, null, true);
            _handShaker = webSocketServerHandshakerFactory.NewHandshaker(request);
            if (_handShaker == null)
            {
                await WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ctx.Channel);
            }
            else
            {
                await _handShaker.HandshakeAsync(ctx.Channel, request);
            }
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        protected virtual async Task HandlerRequestAsync(IChannelHandlerContext ctx, WebSocketFrame frame)
        {
            switch (frame)
            {
                //关闭
                case CloseWebSocketFrame closeWebSocketFrame:
                    if (_handShaker == null) return;
                    await _handShaker.CloseAsync(ctx.Channel, closeWebSocketFrame);
                    break;
                //心跳->Ping
                case PingWebSocketFrame _:
                    await ctx.WriteAndFlushAsync(new PongWebSocketFrame());
                    break;
                //心跳->Poing
                case PongWebSocketFrame _:
                    await ctx.WriteAndFlushAsync(new PingWebSocketFrame());
                    break;
                //文本
                case TextWebSocketFrame textWebSocketFrame:
                    string commandJson = textWebSocketFrame.Content.ToString(Encoding.UTF8);
                    ICommand command = commandJson.JsonToObject<BaseCommand>();
                    ICommandHandler commandHandler = _commandBus.GetCommandHandler(command.CommandHandler);
                    command = (ICommand)commandJson.JsonToObject(commandHandler.CommandType);
                    await commandHandler.HandlerAsync(command, ctx.Channel);
                    break;
                //二进制
                case BinaryWebSocketFrame binaryWebSocketFrame:
                    await ctx.WriteAndFlushAsync(new BinaryWebSocketFrame(binaryWebSocketFrame.Content));
                    break;
            }
        }
        #endregion
    }
}
