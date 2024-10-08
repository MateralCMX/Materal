using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.TTA.SqliteEFRepository
{
    /// <summary>
    /// SqliteEF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqliteEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext) : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext)
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        protected override IDbConnection GetConnection(string connectionString) => new SqliteConnection(connectionString);
    }
}
