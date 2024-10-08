using Materal.Utils.Cache;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.TTA.SqliteEFRepository
{
    /// <summary>
    /// Sqlite缓存EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqliteCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheHelper) : CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext, cacheHelper)
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        protected override IDbConnection GetConnection(string connectionString) => new SqliteConnection(connectionString);
    }
}
