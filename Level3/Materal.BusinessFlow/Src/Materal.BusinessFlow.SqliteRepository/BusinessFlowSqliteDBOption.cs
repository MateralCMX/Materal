using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class BusinessFlowSqliteDBOption : DBOption
    {
        public BusinessFlowSqliteDBOption(SqliteConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        public BusinessFlowSqliteDBOption(string connectionString) : base(connectionString)
        {
        }
        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
    }
}
