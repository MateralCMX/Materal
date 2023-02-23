using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Materal.Utils.Cache;
using Materal.Utils.Redis;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqliteRepository
{
    public abstract class SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType> : CacheEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        protected SqliteCacheEFRepositoryImpl(DbContext dbContext, ICacheHelper cacheManager, RedisManager? redisManager = null) : base(dbContext, cacheManager, redisManager)
        {
        }
    }
}
