using Materal.APP.Core.ConfigModels;
using Microsoft.Extensions.Configuration;
using System;

namespace Materal.APP.Core
{
    public static class ApplicationConfig
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceProvider Services { get; set; }
        public static IConfiguration Config { get; private set; }
        public static WebAPIStartupConfig WebAPIStartupConfig { get; private set; }
        private static GatewayConfigModel _gatewayConfig;
        /// <summary>
        /// 网关
        /// </summary>
        public static GatewayConfigModel GatewayConfig => _gatewayConfig ??= new GatewayConfigModel();
        private static BaseUrlConfigModel _baseUrlConfig;
        /// <summary>
        /// 链接配置
        /// </summary>
        public static BaseUrlConfigModel BaseUrlConfig => _baseUrlConfig ??= new BaseUrlConfigModel();
        /// <summary>
        /// 是否显示异常
        /// </summary>
        public static bool ShowException => Convert.ToBoolean(Config.GetSection(nameof(ShowException)).Value);
        /// <summary>
        /// 是否启用Swagger
        /// </summary>
        public static bool EnableSwagger => Convert.ToBoolean(Config.GetSection(nameof(EnableSwagger)).Value);
        private static ConsulConfigModel _consulConfig;
        /// <summary>
        /// Consul配置
        /// </summary>
        public static ConsulConfigModel ConsulConfig => _consulConfig ??= new ConsulConfigModel();
        private static NLogConfigModel _nLogConfig;
        /// <summary>
        /// NLog配置
        /// </summary>
        public static NLogConfigModel NLogConfig => _nLogConfig ??= new NLogConfigModel();
        private static JWTConfigModel _jwtConfig;
        /// <summary>
        /// 授权配置
        /// </summary>
        public static JWTConfigModel JWTConfig => _jwtConfig ??= new JWTConfigModel();

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void Init(IConfiguration configuration)
        {
            Config = configuration;
        }
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="webAPIStartupConfig"></param>
        /// <returns></returns>
        public static void Init(WebAPIStartupConfig webAPIStartupConfig)
        {
            WebAPIStartupConfig = webAPIStartupConfig;
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return GetService<T>(typeof(T));
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static T GetService<T>(Type serviceType)
        {
            object service = Services?.GetService(serviceType);
            if (service == null) return default;
            if (service is T result)
            {
                return result;
            }
            return default;
        }
    }
}
