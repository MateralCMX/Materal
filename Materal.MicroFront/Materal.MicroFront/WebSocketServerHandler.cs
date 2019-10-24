using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.MicroFront.Common;
using Materal.MicroFront.Common.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.MicroFront.Commands;
using Materal.MicroFront.Common.Extension;
using Materal.MicroFront.Events;
using Microsoft.Extensions.Logging;
using System.Linq;
using Materal.Services;
using Materal.WebSocket.Commands;

namespace Materal.MicroFront
{
    public class WebSocketServerHandler : SimpleChannelInboundHandler<object>
    {
        private const string WebsocketPath = "/websocket";
        private WebSocketServerHandshaker _handShaker;
        private readonly WebSocketConfigModel _webSocketConfig;
        private readonly ILogger<WebSocketServerHandler> _logger;
        private readonly ICommandBus _commandBus;
        public WebSocketServerHandler(ILogger<WebSocketServerHandler> logger, ICommandBus commandBus)
        {
            _logger = logger;
            _commandBus = commandBus;
            _webSocketConfig = ApplicationConfig.WebSocketConfig;
        }
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
        void HandleHttpRequest(IChannelHandlerContext ctx, IFullHttpRequest req)
        {
            if (!req.Result.IsSuccess)
            {
                SendHttpResponse(ctx, req, new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.BadRequest));
                return;
            }
            if (req.Uri.Equals(WebsocketPath))
            {
                var wsFactory = new WebSocketServerHandshakerFactory(
                    GetWebSocketLocation(req), null, true, _webSocketConfig.MaxMessageLength);
                _handShaker = wsFactory.NewHandshaker(req);
                if (_handShaker == null)
                {
                    WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ctx.Channel);
                }
                else
                {
                    _handShaker.HandshakeAsync(ctx.Channel, req);
                }
                ConsoleHelper.MicroFrontServerWriteLine($"新的连接{ctx.Channel.Id}");
                return;
            }
            if (req.Uri.Equals("/favicon.ico"))
            {
                var res = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.NotFound);
                SendHttpResponse(ctx, req, res);
                return;
            }
            var httpHandler = new HttpHandler();
            DefaultFullHttpResponse httpResponse = httpHandler.GetResponse(req);
            SendHttpResponse(ctx, req, httpResponse);
        }
        void HandleWebSocketFrame(IChannelHandlerContext ctx, IByteBufferHolder frame)
        {
            switch (frame)
            {
                case CloseWebSocketFrame _:
                    _handShaker.CloseAsync(ctx.Channel, (CloseWebSocketFrame)frame.Retain());
                    ConsoleHelper.MicroFrontServerWriteLine($"连接{ctx.Channel.Id}已断开");
                    return;
                case PingWebSocketFrame _:
                    ctx.WriteAsync(new PongWebSocketFrame((IByteBuffer)frame.Content.Retain()));
                    return;
                case PongWebSocketFrame _:
                    ctx.WriteAsync(new PingWebSocketFrame((IByteBuffer)frame.Content.Retain()));
                    return;
                case TextWebSocketFrame _:
                    try
                    {
                        string commandString = frame.Content.ReadString(frame.Content.WriterIndex, Encoding.UTF8);
                        var command = commandString.JsonToObject<Command>();
                        if (_webSocketConfig.NotShowCommandBlackList == null || _webSocketConfig.NotShowCommandBlackList.Length == 0 || !_webSocketConfig.NotShowCommandBlackList.Contains(command.HandlerName))
                        {
                            ConsoleHelper.MicroFrontServerWriteLine(command.HandlerName);
                        }

                        if (CanLoginSuccess(command))
                        {
                            Task.Run(async () => await SendCommand(ctx, commandString, command));
                        }
                        else
                        {
                            Task.Run(async () =>
                            {
                                var @event = new ServerErrorEvent
                                {
                                    Status = 401,
                                    Message = "未登录"
                                };
                                await ctx.Channel.SendJsonEventAsync(@event);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        ConsoleHelper.MicroFrontServerErrorWriteLine(ex);
                        _logger.LogCritical(ex, ex.Message);
                    }
                    return;
                case BinaryWebSocketFrame binaryFrame:
                    ctx.WriteAndFlushAsync(binaryFrame.Retain());
                    break;
            }
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        private bool CanLoginSuccess(Command command)
        {
            string token = command.Token;
            if (string.IsNullOrWhiteSpace(token)) return false;
            var authorityService = ApplicationData.GetService<IAuthorityService>();
            return authorityService.IsLogin(token);
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="commandString"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private async Task SendCommand(IChannelHandlerContext ctx, string commandString, Command command)
        {
            try
            {
                await _commandBus.SendAsync(ctx, commandString, command);
            }
            catch (InvalidOperationException ex)
            {
                var @event = new ServerErrorEvent
                {
                    Message = ex.Message
                };
                await ctx.Channel.SendJsonEventAsync(@event);
            }
            catch (Exception ex)
            {
                var @event = new ServerErrorEvent();
                await ctx.Channel.SendJsonEventAsync(@event);
                ConsoleHelper.MicroFrontServerErrorWriteLine(ex);
                _logger.LogCritical(ex, ex.Message);
            }
        }
        /// <summary>
        /// 发送Http返回
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="req"></param>
        /// <param name="res"></param>
        private void SendHttpResponse(IChannelHandlerContext ctx, IFullHttpRequest req, IFullHttpResponse res)
        {
            Task task = ctx.Channel.WriteAndFlushAsync(res);
            if (!HttpUtil.IsKeepAlive(req) || res.Status.Code != 200)
            {
                task.ContinueWith((t, c) => ((IChannelHandlerContext)c).CloseAsync(),
                    ctx, TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine($"{nameof(WebSocketServerHandler)} {0}", e);
            ctx.CloseAsync();
        }
        private string GetWebSocketLocation(IFullHttpRequest req)
        {
            req.Headers.TryGet(HttpHeaderNames.Host, out ICharSequence value);
            string location = value.ToString() + WebsocketPath;
            if (_webSocketConfig.IsSsl)
            {
                return "wss://" + location;
            }
            return "ws://" + location;
        }
    }
}
