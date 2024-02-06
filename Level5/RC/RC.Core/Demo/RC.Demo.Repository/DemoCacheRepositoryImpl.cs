using Materal.Utils.Cache;

namespace RC.Demo.Repository
{
    /// <summary>
    /// Demo缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class DemoCacheRepositoryImpl<TDomain>(DemoDBContext dbContext, ICacheHelper cacheHelper) : RCCacheRepositoryImpl<TDomain, DemoDBContext>(dbContext, cacheHelper), IDemoCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}