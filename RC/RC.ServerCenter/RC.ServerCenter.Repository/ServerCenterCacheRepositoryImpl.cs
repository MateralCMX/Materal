namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class ServerCenterCacheRepositoryImpl<TDomain>(ServerCenterDBContext dbContext, ICacheHelper cacheHelper) : RCCacheRepositoryImpl<TDomain, ServerCenterDBContext>(dbContext, cacheHelper), IServerCenterCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}