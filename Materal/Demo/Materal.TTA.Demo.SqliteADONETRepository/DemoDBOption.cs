using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.TTA.Demo.SqliteADONETRepository
{
    public class DemoDBOption : DBOption
    {
        public DemoDBOption(SqliteConfigModel dbConfig) : base(dbConfig)
        {
        }

        public override IDbConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
