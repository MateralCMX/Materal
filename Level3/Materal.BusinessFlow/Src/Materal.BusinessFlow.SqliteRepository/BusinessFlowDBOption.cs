using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class BusinessFlowDBOption : DBOption
    {
        public BusinessFlowDBOption(SqliteConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        public BusinessFlowDBOption(string connectionString) : base(connectionString)
        {
        }
        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
    }
}
