using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlEFRepository
{
    /// <summary>
    /// MySql缓存EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MySqlCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cacheHelper"></param>
        protected MySqlCacheEFRepositoryImpl(TDBContext dbContext, ICacheHelper cacheHelper) : base(dbContext, cacheHelper)
        {
        }
    }
}
