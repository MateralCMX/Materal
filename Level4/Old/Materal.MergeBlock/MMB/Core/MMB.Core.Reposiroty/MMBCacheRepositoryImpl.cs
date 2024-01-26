using Materal.TTA.SqliteEFRepository;

namespace MMB.Core.Reposiroty
{
    /// <summary>
    /// MMB缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MMBCacheRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheManager) : SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext, cacheManager), IMMBCacheRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
