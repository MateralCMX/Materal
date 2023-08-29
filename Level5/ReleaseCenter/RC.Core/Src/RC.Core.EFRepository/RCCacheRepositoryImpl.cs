using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;
using RC.Core.Domain.Repositories;

namespace RC.Core.EFRepository
{
    /// <summary>
    /// RC缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class RCCacheRepositoryImpl<T, TPrimaryKeyType, TDBContext> : SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>, IRCCacheRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cacheManager"></param>
        public RCCacheRepositoryImpl(TDBContext dbContext, ICacheHelper cacheManager) : base(dbContext, cacheManager)
        {
        }
    }
}
