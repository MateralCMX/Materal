using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Materal.TTA.Demo.SqlServerADONETRepository
{
    public class DemoDBOption : DBOption
    {
        public DemoDBOption(SqlServerConfigModel dbConfig) : base(dbConfig)
        {
        }
        public override IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
