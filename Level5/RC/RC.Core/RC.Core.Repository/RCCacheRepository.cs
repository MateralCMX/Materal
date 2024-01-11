using Materal.TTA.SqliteEFRepository;

namespace RC.Core.Repository
{
    /// <summary>
    /// RC缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class RCCacheRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheManager) : SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext, cacheManager), IRCCacheRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
