namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class EnvironmentServerCacheRepositoryImpl<TDomain>(EnvironmentServerDBContext dbContext, ICacheHelper cacheHelper) : RCCacheRepositoryImpl<TDomain, EnvironmentServerDBContext>(dbContext, cacheHelper), IEnvironmentServerCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}