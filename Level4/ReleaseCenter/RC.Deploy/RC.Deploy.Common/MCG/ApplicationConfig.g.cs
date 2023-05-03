using Materal.BaseCore.Common;
using Materal.TTA.Common.Model;

namespace RC.Deploy.Common
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
                SqliteConfigModel result = MateralCoreConfig.GetValueObject<SqliteConfigModel>(nameof(DBConfig), new SqliteConfigModel {Source = "./Deploy.db"});
                if (result.Source.StartsWith("./"))
                {
                    result.Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, result.Source[2..]);
                }
                return result;
            }
        }
    }
}
