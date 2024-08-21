using Materal.Utils.Cache;

namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServer缓存EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqlServerCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cacheManager"></param>
        protected SqlServerCacheEFRepositoryImpl(TDBContext dbContext, ICacheHelper cacheManager) : base(dbContext, cacheManager)
        {
        }
    }
}
