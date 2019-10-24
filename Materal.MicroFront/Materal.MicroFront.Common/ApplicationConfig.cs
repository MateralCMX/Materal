using Materal.ConfigurationHelper;
using Materal.MicroFront.Common.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Materal.MicroFront.Common
{
    public static class ApplicationConfig
    {
        #region 配置对象
        private static IConfiguration _configuration;
        private const string DefaultConfigFileName = "appsetting";
        private const string DefaultConfigFileSuffix = "json";
        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    ConfigurationBuilder();
                }
                return _configuration;
            }
        }
        /// <summary>
        /// 配置生成
        /// </summary>
        /// <param name="configTarget">配置目标</param>
        /// <returns></returns>
        public static void ConfigurationBuilder(string configTarget = null)
        {
            string appConfigFile = string.IsNullOrEmpty(configTarget) ?
                $"{DefaultConfigFileName}.{DefaultConfigFileSuffix}"
                : $"{DefaultConfigFileName}.{configTarget}.{DefaultConfigFileSuffix}";
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
        }
        #endregion
        #region 配置
        private static WebSocketConfigModel _webSocketConfig;
        /// <summary>
        /// WebSocket配置对象
        /// </summary>
        public static WebSocketConfigModel WebSocketConfig => _webSocketConfig ?? (_webSocketConfig = new WebSocketConfigModel
        {
            CertificatePassword = Configuration["Application:MicroFrontServer:CertificatePassword"],
            CertificatePath = Configuration["Application:MicroFrontServer:CertificatePath"],
            Host = Configuration["Application:MicroFrontServer:Host"],
            Port = Convert.ToInt32(Configuration["Application:MicroFrontServer:Port"]),
            IsSsl = Convert.ToBoolean(Configuration["Application:MicroFrontServer:IsSsl"]),
            UserLibuv = Convert.ToBoolean(Configuration["Application:MicroFrontServer:UserLibuv"]),
            MaxMessageLength = Convert.ToInt32(Configuration["Application:MicroFrontServer:MaxMessageLength"]),
            NotShowCommandBlackList = Configuration.GetArrayValue("Application:MicroFrontServer:NotShowCommandBlackList").ToArray()
        });
        private static string _operatingPassword;
        /// <summary>
        /// 操作密码
        /// </summary>
        public static string OperatingPassword => _operatingPassword ?? (_operatingPassword = Configuration["Application:OperatingPassword"]);

        private static string _systemName;
        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SystemName => _systemName ?? (_systemName = Configuration["Application:SystemName"]);
        #endregion
    }
}
