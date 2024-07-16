namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存服务
    /// </summary>
    public class ContextCacheService(IContextCachePersistence persistence, IServiceProvider serviceProvider) : IContextCacheService
    {
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <returns></returns>
        public IContextCache BeginContextCache<TRestorer>()
            where TRestorer : IContextRestorer
        {
            ContextCacheGroupModel groupInfo = new()
            {
                ID = Guid.NewGuid(),
                RestorerType = typeof(TRestorer)
            };
            IContextCache contextCache = groupInfo.CreateContextCache(persistence);
            return contextCache;
        }
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <returns></returns>
        public async Task<IContextCache> BeginContextCacheAsync<TRestorer>()
            where TRestorer : IContextRestorer
        {
            ContextCacheGroupModel groupInfo = new()
            {
                ID = Guid.NewGuid(),
                RestorerType = typeof(TRestorer)
            };
            IContextCache contextCache = groupInfo.CreateContextCache(persistence);
            await Task.CompletedTask;
            return contextCache;
        }
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public IContextCache BeginContextCache<TRestorer, TContext>(TContext? context)
            where TRestorer : IContextRestorer
            where TContext : class, new()
        {
            IContextCache contextCache = BeginContextCache<TRestorer>();
            contextCache.Begin(context);
            return contextCache;
        }
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IContextCache> BeginContextCacheAsync<TRestorer, TContext>(TContext? context)
            where TRestorer : IContextRestorer
            where TContext : class, new()
        {
            IContextCache contextCache = await BeginContextCacheAsync<TRestorer>();
            await contextCache.BeginAsync(context);
            return contextCache;
        }
        /// <summary>
        /// 重新开始
        /// </summary>
        /// <returns></returns>
        public async Task RenewAsync()
        {
            IEnumerable<ContextCacheGroupModel> groupInfos = persistence.GetAllGroupInfo();
            foreach (ContextCacheGroupModel groupInfo in groupInfos)
            {
                IContextCache contextCache = groupInfo.CreateContextCache(persistence);
                IContextRestorer contextRestorer = groupInfo.CreateContextRestorer(serviceProvider);
                await contextRestorer.RenewAsync(contextCache);
            }
        }
    }
}
