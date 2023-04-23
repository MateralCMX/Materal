using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common.Models;

namespace Materal.BaseCore.WebAPI.Common
{
    /// <summary>
    /// WebAPI配置
    /// </summary>
    public static class WebAPIConfig
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public static string AppName => MateralCoreConfig.GetValue(nameof(AppName), "MateralCoreApplication");
        /// <summary>
        /// 应用标题
        /// </summary>
        public static string AppTitle => MateralCoreConfig.GetValue(nameof(AppTitle), "MateralCore程序");
        /// <summary>
        /// Swagger配置
        /// </summary>
        public static SwaggerConfigModel SwaggerConfig => MateralCoreConfig.GetValueObject(nameof(SwaggerConfig), new SwaggerConfigModel());
        /// <summary>
        /// 启用鉴权
        /// </summary>
        public static bool EnableAuthentication => MateralCoreConfig.GetValueObject(nameof(EnableAuthentication), true);
        private static UrlConfigModel? _baseUrlConfig;
        /// <summary>
        /// 链接配置
        /// </summary>
        public static UrlConfigModel BaseUrlConfig => _baseUrlConfig ??= new()
        {
            Url = MateralCoreConfig.GetValue("URLS", "http://localhost:5000")
        };
        private static UrlConfigModel? _externalUrl;
        /// <summary>
        /// 外部地址
        /// </summary>
        public static UrlConfigModel ExternalUrl => _externalUrl ??= new()
        {
            Url = MateralCoreConfig.GetValue(nameof(ExternalUrl), "http://localhost:5000")
        };
        /// <summary>
        /// Consul配置
        /// </summary>
        public static ConsulConfigModel ConsulConfig => MateralCoreConfig.GetValueObject<ConsulConfigModel>(nameof(ConsulConfig));
    }
}
