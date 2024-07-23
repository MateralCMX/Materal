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
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public IContextCache BeginContextCache<TRestorer, TContext>(TContext? context)
            where TRestorer : IContextRestorer
            where TContext : class, new()
        {
            ContextCacheGroupModel groupInfo = new()
            {
                ID = Guid.NewGuid(),
                RestorerType = typeof(TRestorer)
            };
            IContextCache contextCache = groupInfo.CreateContextCache(persistence);
            contextCache.Begin(context);
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
