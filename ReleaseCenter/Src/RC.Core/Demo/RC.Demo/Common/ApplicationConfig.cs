using Materal.TTA.SqliteRepository.Model;
using RC.Core.Common;

namespace RC.Demo.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class ApplicationConfig
    {
        private static SqliteConfigModel? _dbConfig;
        /// <summary>
        /// 数据库配置
        /// </summary>
        public static SqliteConfigModel DBConfig
        {
            get
            {
                if (_dbConfig != null) return _dbConfig;
                _dbConfig = RCConfig.GetValueObject<SqliteConfigModel>(nameof(DBConfig));
                if (_dbConfig.Source.StartsWith("./"))
                {
                    _dbConfig.Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dbConfig.Source[2..]);
                }
                return _dbConfig;
            }
        }
    }
}
