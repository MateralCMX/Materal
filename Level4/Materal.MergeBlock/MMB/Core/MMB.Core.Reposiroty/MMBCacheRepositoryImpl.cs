using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;

namespace MMB.Core.Reposiroty
{
    /// <summary>
    /// MBC缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MMBCacheRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheManager) : SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext, cacheManager), ICacheMMBRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
