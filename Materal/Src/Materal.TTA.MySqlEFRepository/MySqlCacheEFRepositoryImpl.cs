using Materal.Utils.Cache;
using MySql.Data.MySqlClient;
using System.Data;

namespace Materal.TTA.MySqlEFRepository
{
    /// <summary>
    /// MySql缓存EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MySqlCacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheHelper) : CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext, cacheHelper)
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        protected override IDbConnection GetConnection(string connectionString) => new MySqlConnection(connectionString);
    }
}
