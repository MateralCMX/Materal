using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Materal.BusinessFlow.SqlServerRepository
{
    public class BusinessFlowSqlServerDBOption : DBOption
    {
        public BusinessFlowSqlServerDBOption(SqlServerConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        public BusinessFlowSqlServerDBOption(string connectionString) : base(connectionString)
        {
        }
        public override IDbConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}
