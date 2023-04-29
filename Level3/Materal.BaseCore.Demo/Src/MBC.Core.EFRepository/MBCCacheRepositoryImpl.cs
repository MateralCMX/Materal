using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;

namespace MBC.Core.EFRepository
{
    /// <summary>
    /// MBC缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class MBCCacheRepositoryImpl<T, TPrimaryKeyType, TDBContext> : SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cacheManager"></param>
        public MBCCacheRepositoryImpl(TDBContext dbContext, ICacheHelper cacheManager) : base(dbContext, cacheManager)
        {
        }
    }
}
