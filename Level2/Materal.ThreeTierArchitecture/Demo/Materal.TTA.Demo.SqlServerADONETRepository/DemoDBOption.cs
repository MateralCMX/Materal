using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;

namespace Materal.TTA.Demo.SqlServerADONETRepository
{
    public class DemoDBOption : DBOption
    {
        public DemoDBOption(SqlServerConfigModel dbConfig) : base(dbConfig)
        {
        }
    }
}
