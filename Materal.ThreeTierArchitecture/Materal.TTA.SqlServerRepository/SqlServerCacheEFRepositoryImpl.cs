using Materal.CacheHelper;
using Materal.RedisHelper;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public abstract class SqlServerCacheEFRepositoryImpl<T, TPrimaryKeyType> : CacheEFRepositoryImpl<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        protected SqlServerCacheEFRepositoryImpl(DbContext dbContext, RedisManager redisManager, ICacheManager cacheManager) : base(dbContext, redisManager, cacheManager)
        {
        }
    }
}
