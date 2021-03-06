﻿using System;
using System.IO;
using Materal.APP.Core.Models;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Environment.Common
{
    public class ConfigCenterEnvironmentConfig
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
            const string appConfigFile = "appsettings.Development.json";
#else
            const string appConfigFile = "appsettings.json";
#endif
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
        }
        /// <summary>
        /// 设置配置对象
        /// </summary>
        /// <param name="configuration"></param>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        #region 配置
        private static SqliteConfigModel _sqliteConfig;
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig => _sqliteConfig ??= new SqliteConfigModel
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration["SqliteConfig:FilePath"]),
            Password = Configuration["SqliteConfig:Password"],
            Version = Configuration["SqliteConfig:Version"]
        };

        private static ServerInfoModel _serverInfo;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static ServerInfoModel ServerInfo => _serverInfo ??= new ServerInfoModel
        {
            PublicUrl = Configuration["ServerInfo:PublicUrl"],
            Url = Configuration["ServerInfo:Url"],
            Key = Configuration["ServerInfo:Key"],
            Name = Configuration["ServerInfo:Name"]
        };
        /// <summary>
        /// 配置中心地址
        /// </summary>
        public static string ConfigCenterUrl{ get; set; }
        #endregion
    }
}
