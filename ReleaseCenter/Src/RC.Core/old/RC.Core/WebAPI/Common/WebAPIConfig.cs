using Materal.TTA.SqliteRepository.Model;
using RC.Core.Common;
using RC.Core.WebAPI.Common.Models;

namespace RC.Core.WebAPI.Common
{
    /// <summary>
    /// WebAPI配置
    /// </summary>
    public static class WebAPIConfig
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public static string AppName => RCConfig.GetValue(nameof(AppName), "RCApplication");
        /// <summary>
        /// 应用标题
        /// </summary>
        public static string AppTitle => RCConfig.GetValue(nameof(AppTitle), "发布中心程序");
        /// <summary>
        /// 启用Swagger
        /// </summary>
        public static bool EnableSwagger => RCConfig.GetValueObject(nameof(EnableSwagger), true);
        /// <summary>
        /// 日志数据库配置
        /// </summary>
        public static SqliteConfigModel LogDBConfig => RCConfig.GetValueObject<SqliteConfigModel>(nameof(LogDBConfig));
        private static UrlConfigModel? _baseUrlConfig;
        /// <summary>
        /// 链接配置
        /// </summary>
        public static UrlConfigModel BaseUrlConfig => _baseUrlConfig ??= new()
        {
            Url = RCConfig.GetValue("URLS", "http://localhost:5000")
        };
        private static UrlConfigModel? _externalUrl;
        /// <summary>
        /// 外部地址
        /// </summary>
        public static UrlConfigModel ExternalUrl => _externalUrl ??= new()
        {
            Url = RCConfig.GetValue(nameof(ExternalUrl), "http://localhost:5000")
        };
        /// <summary>
        /// Consul配置
        /// </summary>
        public static ConsulConfigModel ConsulConfig => RCConfig.GetValueObject<ConsulConfigModel>(nameof(ConsulConfig));
    }
}
