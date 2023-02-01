using Materal.DotNetty.Server.Core;
using Microsoft.Extensions.Configuration;
using System;

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
        /// <summary>
        /// 配置生成
        /// </summary>
        private static void ConfigurationBuilder()
        {
#if DEBUG
            const string appConfigFile = "appsetting.Development.json";
#else
            const string appConfigFile = "appsetting.json";
#endif
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
        public static ServerConfig ServerConfig => _serverConfig ??= new ServerConfig
        {
            Host = Configuration["ServerConfig:Host"],
            Port = Convert.ToInt32(Configuration["ServerConfig:Port"])
        };
        private static ServerConfig _centerServerConfig;
        /// <summary>
        /// 中心服务配置
        /// </summary>
        public static ServerConfig CenterServerConfig => _centerServerConfig ??= new ServerConfig
        {
            Host = Configuration["CenterServerConfig:Host"],
            Port = Convert.ToInt32(Configuration["CenterServerConfig:Port"])
        };
        private static string _winRarPath;
        /// <summary>
        /// WinRar路径
        /// </summary>
        public static string WinRarPath => _winRarPath ??= Configuration["WinRarPath"]; 
        private static string _conDepName;
        /// <summary>
        /// 名称
        /// </summary>
        public static string ConDepName => _conDepName ??= Configuration["ConDepName"];
        #endregion
    }
}
