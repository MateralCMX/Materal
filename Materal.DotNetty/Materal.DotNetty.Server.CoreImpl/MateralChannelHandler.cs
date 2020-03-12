using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.DotNetty.Server.Core;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class MateralChannelHandler : SimpleChannelInboundHandler<IByteBufferHolder>, IMateralChannelHandler
    {
        private readonly List<HandlerContext> handlers = new List<HandlerContext>();
        public event Action<Exception> OnException;
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
        
        public void AddLastHandler(HandlerContext handler)
        {
            handlers.Add(handler);
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <returns></returns>
        private HandlerContext GetHandler()
        {
            for (var i = 0; i < handlers.Count; i++)
            {
                handlers[i].ShowException = OnException;
                if(i - 1 >= 0)
                {
                    handlers[i - 1].SetNext(handlers[i]);
                }
            }
            return handlers[0];
        }
    }
}
