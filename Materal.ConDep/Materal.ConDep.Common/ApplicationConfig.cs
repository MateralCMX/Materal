using System;
using Materal.DotNetty.Server.Core;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.Extensions.Configuration;

namespace Materal.ConDep.Common
{
    public class ApplicationConfig
    {
        #region 配置对象
        private static IConfiguration _configuration;
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
        private const string DefaultConfigFileName = "appsetting";
        private const string DefaultConfigFileSuffix = "json";
        /// <summary>
        /// 配置生成
        /// </summary>
        /// <param name="targetConfig"></param>
        private static void ConfigurationBuilder(string targetConfig = null)
        {
            string appConfigFile = string.IsNullOrEmpty(targetConfig) ?
                $"{DefaultConfigFileName}.{DefaultConfigFileSuffix}" :
                $"{DefaultConfigFileName}.{targetConfig}.{DefaultConfigFileSuffix}";
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
        }
        #endregion
        #region 配置
        private static ServerConfig _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static ServerConfig ServerConfig => _serverConfig ?? (_serverConfig = new ServerConfig
        {
            Host = Configuration["ServerConfig:Host"],
            Port = Convert.ToInt32(Configuration["ServerConfig:Port"])
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
