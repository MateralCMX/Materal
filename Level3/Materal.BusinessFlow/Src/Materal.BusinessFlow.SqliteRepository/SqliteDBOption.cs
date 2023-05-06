using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class SqliteDBOption : DBOption
    {
        public SqliteDBOption(SqliteConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        public SqliteDBOption(string connectionString) : base(connectionString)
        {
        }
        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
    }
}
