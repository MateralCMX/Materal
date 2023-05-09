using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Materal.BusinessFlow.SqlServerRepository
{
    public class BusinessFlowDBOption : DBOption
    {
        public BusinessFlowDBOption(SqlServerConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        public BusinessFlowDBOption(string connectionString) : base(connectionString)
        {
        }
        public override IDbConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}
