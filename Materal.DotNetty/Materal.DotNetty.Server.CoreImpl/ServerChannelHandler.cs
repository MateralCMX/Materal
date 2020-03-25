using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Server.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class ServerChannelHandler : SimpleChannelInboundHandler<IByteBufferHolder>, IServerChannelHandler
    {
        private readonly List<ServerHandlerContext> handlers = new List<ServerHandlerContext>();
        public event Action<Exception> OnException;
        public event Action<string> OnMessage;
        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder)
        {
            byteBufferHolder.Retain(byte.MaxValue);
            Task.Run(async () =>
            {
                try
                {
                    await GetHandler().HandlerAsync(ctx, byteBufferHolder);
                }
                catch (Exception exception)
                {
                    OnException?.Invoke(exception);
                }
            });
        }
        public void AddLastHandler(ServerHandlerContext handler)
        {
            handlers.Add(handler);
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <returns></returns>
        private ServerHandlerContext GetHandler()
        {
            for (var i = 0; i < handlers.Count; i++)
            {
                handlers[i].OnException = OnException;
                handlers[i].OnMessage = OnMessage;
                if(i - 1 >= 0)
                {
                    handlers[i - 1].SetNext(handlers[i]);
                }
            }
            return handlers[0];
        }
    }
}
