using Deploy.Common.Models;
using Materal.APP.Core;
using Materal.TTA.SqliteRepository.Model;

namespace Deploy.Common
{
    public static class DeployConfig
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public static string ServiceName { get; set; }
        /// <summary>
        /// 应用程序白名单
        /// </summary>
        public static string[] ApplicationNameWhiteList { get; } =
        {
            "api",
            "swagger",
            "Deploy"
        };
        private static DeployConfigModel _deployConfig;
        /// <summary>
        /// 发布服务配置
        /// </summary>
        public static DeployConfigModel DeployServerConfig => _deployConfig ??= new DeployConfigModel();
        private static RewriteConfigModel _rewriteConfig;
        /// <summary>
        /// 重写配置
        /// </summary>
        public static RewriteConfigModel RewriteConfig => _rewriteConfig ??= new RewriteConfigModel();
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig => new SqliteConfigModel
        {
            FilePath = GetConfigValue("SqliteDB:FilePath"),
            Password = null,
            Version = null
        };
        /// <summary>
        /// WinRar路径
        /// </summary>
        public static string WinRarPath => GetConfigValue(nameof(WinRarPath));
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection(key).Value;
        }
    }
}
