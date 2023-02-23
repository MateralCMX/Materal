using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Materal.Utils.Cache;
using Materal.Utils.Redis;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public abstract class SqlServerCacheEFRepositoryImpl<T, TPrimaryKeyType> : CacheEFRepositoryImpl<T, TPrimaryKeyType> 
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        protected SqlServerCacheEFRepositoryImpl(DbContext dbContext, ICacheHelper cacheManager, RedisManager? redisManager = null) : base(dbContext, cacheManager, redisManager)
        {
        }
    }
}
