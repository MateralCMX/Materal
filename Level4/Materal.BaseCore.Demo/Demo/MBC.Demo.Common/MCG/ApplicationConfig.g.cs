using Materal.BaseCore.Common;
using Materal.TTA.SqliteEFRepository;

namespace MBC.Demo.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public partial class ApplicationConfig
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        public static SqliteConfigModel DBConfig
        {
            get
            {
                SqliteConfigModel result = MateralCoreConfig.GetConfigItem<SqliteConfigModel>(nameof(DBConfig), new SqliteConfigModel {Source = "./Demo.db"});
                if (result.Source.StartsWith("./"))
                {
                    result.Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, result.Source[2..]);
                }
                return result;
            }
        }
    }
}
