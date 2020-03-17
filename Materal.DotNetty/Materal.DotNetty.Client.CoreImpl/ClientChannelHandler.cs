using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Concurrency;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Client.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client.CoreImpl
{
    public class ClientChannelHandler : SimpleChannelInboundHandler<object>, IClientChannelHandler
    {
        public readonly WebSocketClientHandshaker handshaker;
        public readonly TaskCompletionSource completionSource;
        private readonly List<ClientHandlerContext> handlers = new List<ClientHandlerContext>();
        public event Action<Exception> OnException;
        public event Action<string> OnMessage;
        public event Action<IChannelHandlerContext> OnChannelInactive;
        public Task HandshakeCompletion => completionSource.Task;

        public ClientChannelHandler(WebSocketClientHandshaker handshaker)
        {
            this.handshaker = handshaker;
            completionSource = new TaskCompletionSource();
        }
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            handshaker.HandshakeAsync(ctx.Channel).LinkOutcome(completionSource);
        }
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            OnChannelInactive?.Invoke(context);
        }
        protected override void ChannelRead0(IChannelHandlerContext context, object data)
        {
            Task.Run(async () =>
            {
                try
                {
                    await GetHandler().HandlerAsync(context, data);
                }
                catch (Exception exception)
                {
                    OnException?.Invoke(exception);
                }
            });
            //IChannel ch = context.Channel;
            //if (!handshaker.IsHandshakeComplete)
            //{
            //    try
            //    {
            //        handshaker.FinishHandshake(ch, (IFullHttpResponse)data);
            //        Console.WriteLine("WebSocket Client connected!");
            //        completionSource.TryComplete();
            //    }
            //    catch (WebSocketHandshakeException e)
            //    {
            //        Console.WriteLine("WebSocket Client failed to connect");
            //        completionSource.TrySetException(e);
            //    }
            //    return;
            //}
            //if (data is IFullHttpResponse response)
            //{
            //    throw new InvalidOperationException($"Unexpected FullHttpResponse (getStatus={response.Status}, content={response.Content.ToString(Encoding.UTF8)})");
            //}
            //if (data is TextWebSocketFrame textFrame)
            //{
            //    Console.WriteLine($"WebSocket Client received message: {textFrame.Text()}");
            //}
            //else if (data is PongWebSocketFrame)
            //{
            //    Console.WriteLine("WebSocket Client received pong");
            //}
            //else if (data is CloseWebSocketFrame)
            //{
            //    Console.WriteLine("WebSocket Client received closing");
            //    ch.CloseAsync();
            //}
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            OnException?.Invoke(exception);
            completionSource.TrySetException(exception);
            ctx.CloseAsync();
        }

        public void AddLastHandler(ClientHandlerContext handler)
        {
            handlers.Add(handler);
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <returns></returns>
        private ClientHandlerContext GetHandler()
        {
            for (var i = 0; i < handlers.Count; i++)
            {
                handlers[i].OnException = OnException;
                handlers[i].OnMessage = OnMessage;
                if (i - 1 >= 0)
                {
                    handlers[i - 1].SetNext(handlers[i]);
                }
            }
            return handlers[0];
        }
    }
}
