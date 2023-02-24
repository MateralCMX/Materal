using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;

namespace MBC.Core.EFRepository
{
    /// <summary>
    /// MBC缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class MBCCacheRepositoryImpl<T, TPrimaryKeyType> : CacheEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cacheManager"></param>
        public MBCCacheRepositoryImpl(DbContext dbContext, ICacheHelper cacheManager) : base(dbContext, cacheManager, null)
        {
        }
    }
}
