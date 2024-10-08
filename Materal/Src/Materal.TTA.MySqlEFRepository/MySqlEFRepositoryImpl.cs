using MySql.Data.MySqlClient;
using System.Data;

namespace Materal.TTA.MySqlEFRepository
{
    /// <summary>
    /// MySqlEF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MySqlEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext) : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext)
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        protected override IDbConnection GetConnection(string connectionString) => new MySqlConnection(connectionString);
    }
}
