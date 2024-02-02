using Materal.Utils.Cache;

namespace RC.Core.Repository
{
    /// <summary>
    /// RC缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class RCCacheRepositoryImpl<TDomain, TDBContext>(TDBContext dbContext, ICacheHelper cacheHelper) : SqliteCacheEFRepositoryImpl<TDomain, Guid, TDBContext>(dbContext, cacheHelper), IRCCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
        where TDBContext : DbContext
    {
    }
}