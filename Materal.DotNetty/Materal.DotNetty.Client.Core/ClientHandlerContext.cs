using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client.Core
{
    public abstract class ClientHandlerContext
    {
        public Action<Exception> OnException;
        public Action<string> OnMessage;
        private ClientHandlerContext _handlerContext;
        private bool _canNext = true;
        /// <summary>
        /// 是否可以执行下一步标识
        /// </summary>
        protected bool CanNext => _handlerContext != null && _canNext;
        /// <summary>
        /// 设置下一步
        /// </summary>
        /// <param name="context"></param>
        public void SetNext(ClientHandlerContext context)
        {
            _handlerContext = context;
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task HandlerAsync<T>(IChannelHandlerContext ctx, object data, Func<IChannelHandlerContext,T,Task> action)
        {
            if(data is T target && action != null)
            {
                await action(ctx, target);
            }
            await NextAsync(ctx, data);
        }
        /// <summary>
        /// 执行下一步
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task NextAsync(IChannelHandlerContext ctx, object data)
        {
            if (CanNext)
            {
                await _handlerContext.HandlerAsync(ctx, data);
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
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract Task HandlerAsync(IChannelHandlerContext ctx, object data);
    }
}
