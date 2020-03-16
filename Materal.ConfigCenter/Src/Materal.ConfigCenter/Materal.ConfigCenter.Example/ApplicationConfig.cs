using Microsoft.Extensions.Configuration;
using System;
using Materal.DotNetty.Client.Core;

namespace Materal.ConfigCenter.Example
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
        /// <summary>
        /// 配置生成
        /// </summary>
        private static void ConfigurationBuilder()
        {
            const string appConfigFile = "appsetting.json";
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
        }
        #endregion
        #region 配置
        private static ClientConfig _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static ClientConfig ServerConfig => _serverConfig ?? (_serverConfig = new ClientConfig
        {
            Host = Configuration["ClientConfig:Host"],
            Port = Convert.ToInt32(Configuration["ClientConfig:Port"])
        });
        #endregion
    }
}
