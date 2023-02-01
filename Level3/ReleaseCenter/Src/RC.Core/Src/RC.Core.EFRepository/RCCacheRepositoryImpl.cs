using Materal.CacheHelper;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;

namespace RC.Core.EFRepository
{
    /// <summary>
    /// RC缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class RCCacheRepositoryImpl<T, TPrimaryKeyType> : SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cacheManager"></param>
        public RCCacheRepositoryImpl(DbContext dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager, null)
        {
        }
    }
}
