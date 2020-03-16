using System;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Materal.DotNetty.Server.Core
{
    public abstract class ServerHandlerContext
    {
        public Action<Exception> OnException;
        public Action<string> OnMessage;
        private ServerHandlerContext _handlerContext;
        private bool _canNext = true;
        /// <summary>
        /// 是否可以执行下一步标识
        /// </summary>
        protected bool CanNext => _handlerContext != null && _canNext;
        /// <summary>
        /// 设置下一步
        /// </summary>
        /// <param name="context"></param>
        public void SetNext(ServerHandlerContext context)
        {
            _handlerContext = context;
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="byteBufferHolder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task HandlerAsync<T>(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder, Func<IChannelHandlerContext,T,Task> action)
        {
            if(byteBufferHolder is T target && action != null)
            {
                await action(ctx, target);
            }
            await NextAsync(ctx, byteBufferHolder);
        }
        /// <summary>
        /// 执行下一步
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="byteBufferHolder"></param>
        /// <returns></returns>
        public async Task NextAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder)
        {
            if (CanNext)
            {
                await _handlerContext.HandlerAsync(ctx, byteBufferHolder);
            }
        }
        /// <summary>
        /// 停止处理
        /// </summary>
        protected void StopHandler()
        {
            _canNext = false;
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="byteBufferHolder"></param>
        /// <returns></returns>
        public abstract Task HandlerAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder);
    }
}
