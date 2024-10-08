using Microsoft.Data.SqlClient;
using System.Data;

namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServerEF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqlServerEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext) : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext)
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        protected override IDbConnection GetConnection(string connectionString) => new SqlConnection(connectionString);
    }
}
