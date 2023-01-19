using Materal.BaseCore.Common;
using Materal.TTA.SqliteRepository.Model;
using RC.Deploy.Common.Models;

namespace RC.Deploy.Common
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
                _dbConfig = MateralCoreConfig.GetValueObject<SqliteConfigModel>(nameof(DBConfig));
                if (_dbConfig.Source.StartsWith("./"))
                {
                    _dbConfig.Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dbConfig.Source[2..]);
                }
                return _dbConfig;
            }
        }
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public static string UploadFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MateralCoreConfig.GetValue(nameof(UploadFilePath), "UploadFiles"));
        /// <summary>
        /// WinRar路径
        /// </summary>
        public static string WinRarPath => MateralCoreConfig.GetValue(nameof(WinRarPath), string.Empty);
        private static RewriteConfigModel? _rewriteConfig;
        /// <summary>
        /// 重写配置
        /// </summary>
        public static RewriteConfigModel RewriteConfig
        {
            get
            {
                if (_rewriteConfig != null) return _rewriteConfig;
                _rewriteConfig = MateralCoreConfig.GetValueObject<RewriteConfigModel>(nameof(RewriteConfig));
                return _rewriteConfig;
            }
        }
        /// <summary>
        /// 应用程序白名单
        /// </summary>
        public static string[] ApplicationNameWhiteList { get; } =
        {
            "api",
            "hubs",
            "swagger",
            "Deploy"
        };
    }
}
