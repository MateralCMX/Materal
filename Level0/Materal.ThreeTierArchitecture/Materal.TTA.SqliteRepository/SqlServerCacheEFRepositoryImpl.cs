using Materal.CacheHelper;
using Materal.RedisHelper;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqliteRepository
{
    public abstract class SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType> : CacheEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        protected SqliteCacheEFRepositoryImpl(DbContext dbContext, ICacheManager cacheManager, RedisManager? redisManager = null) : base(dbContext, cacheManager, redisManager)
        {
        }
    }
}
