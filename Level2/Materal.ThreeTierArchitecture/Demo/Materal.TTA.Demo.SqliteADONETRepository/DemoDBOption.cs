using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;

namespace Materal.TTA.Demo.SqliteADONETRepository
{
    public class DemoDBOption : DBOption
    {
        public DemoDBOption(SqliteConfigModel dbConfig) : base(dbConfig)
        {
        }
    }
}
