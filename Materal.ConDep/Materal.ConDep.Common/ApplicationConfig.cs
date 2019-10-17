using Materal.ConDep.Common.Models;
using Materal.ConfigurationHelper;
using Microsoft.Extensions.Configuration;
using System;

namespace Materal.ConDep.Common
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
            CertificatePassword = Configuration["Application:ConDepServer:CertificatePassword"],
            CertificatePath = Configuration["Application:ConDepServer:CertificatePath"],
            Host = Configuration["Application:ConDepServer:Host"],
            Port = Convert.ToInt32(Configuration["Application:ConDepServer:Port"]),
            IsSsl = Convert.ToBoolean(Configuration["Application:ConDepServer:IsSsl"]),
            UserLibuv = Convert.ToBoolean(Configuration["Application:ConDepServer:UserLibuv"]),
            MaxMessageLength = Convert.ToInt32(Configuration["Application:ConDepServer:MaxMessageLength"]),
            NotShowCommandBlackList = Configuration.GetArrayValue("Application:ConDepServer:NotShowCommandBlackList").ToArray()
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
