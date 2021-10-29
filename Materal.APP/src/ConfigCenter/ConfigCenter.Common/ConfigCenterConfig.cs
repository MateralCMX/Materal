using Materal.APP.Core;
using Materal.APP.Core.ConfigModels;
using Materal.TTA.SqliteRepository.Model;

namespace ConfigCenter.Common
{
    public class ConfigCenterConfig
    {
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig => new SqliteConfigModel
        {
            FilePath = GetConfigValue("SqliteDB:FilePath"),
            Password = null,
            Version = null
        };
        private static TFMSConfigModel _tfmsConfig;
        /// <summary>
        /// 事件分发模型
        /// </summary>
        public static TFMSConfigModel TFMSConfig => _tfmsConfig ??= new TFMSConfigModel();
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
