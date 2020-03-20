using Materal.DotNetty.Server.Core;
using Microsoft.Extensions.Configuration;
using System;
using Materal.TTA.SqliteRepository.Model;

namespace Materal.ConfigCenter.ConfigServer.Common
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
        public static ServerConfig ServerConfig => _serverConfig ?? (_serverConfig = new ServerConfig
        {
            Host = Configuration["ServerConfig:Host"],
            Port = Convert.ToInt32(Configuration["ServerConfig:Port"])
        });
        private static ProtalServerConfigModel _protalServerConfig;
        /// <summary>
        /// 配置服务
        /// </summary>
        public static ProtalServerConfigModel ProtalServerConfig => _protalServerConfig ?? (_protalServerConfig = new ProtalServerConfigModel
        {
            ProtalUrl = Configuration["ProtalServerConfig:ProtalUrl"],
            Name = Configuration["ProtalServerConfig:Name"]
        });
        private static SqliteConfigModel _sqliteConfig;
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig => _sqliteConfig ?? (_sqliteConfig = new SqliteConfigModel
        {
            FilePath = Configuration["SqliteConfig:FilePath"],
            Password = Configuration["SqliteConfig:Password"],
            Version = Configuration["SqliteConfig:Version"]
        });
        #endregion
    }
}
