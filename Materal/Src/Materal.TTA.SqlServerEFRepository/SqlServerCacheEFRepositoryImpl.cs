using Materal.Utils.Cache;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServer缓存EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqlServerCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheManager) : CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext, cacheManager)
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        protected override IDbConnection GetConnection(string connectionString) => new SqlConnection(connectionString);
    }
}
