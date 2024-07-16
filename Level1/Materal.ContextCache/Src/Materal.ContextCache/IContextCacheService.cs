namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存服务
    /// </summary>
    public interface IContextCacheService
    {
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <returns></returns>
        IContextCache BeginContextCache<TRestorer>()
            where TRestorer : IContextRestorer;
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <returns></returns>
        Task<IContextCache> BeginContextCacheAsync<TRestorer>()
            where TRestorer : IContextRestorer;
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        IContextCache BeginContextCache<TRestorer, TContext>(TContext context)
            where TRestorer : IContextRestorer
            where TContext : class, new();
        /// <summary>
        /// 开始上下文缓存
        /// </summary>
        /// <typeparam name="TRestorer"></typeparam>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<IContextCache> BeginContextCacheAsync<TRestorer, TContext>(TContext context)
            where TRestorer : IContextRestorer
            where TContext : class, new();
        /// <summary>
        /// 重新开始
        /// </summary>
        /// <returns></returns>
        Task RenewAsync();
    }
}
