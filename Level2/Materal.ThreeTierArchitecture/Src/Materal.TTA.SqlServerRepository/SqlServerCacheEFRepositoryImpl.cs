using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Materal.Utils.Cache;
using Materal.Utils.Redis;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public abstract class SqlServerCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        protected SqlServerCacheEFRepositoryImpl(TDBContext dbContext, ICacheHelper cacheManager, RedisManager? redisManager = null) : base(dbContext, cacheManager, redisManager)
        {
        }
    }
}
