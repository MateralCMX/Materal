﻿using Materal.APP.Core.Models;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Deploy.Common
{
    public static class DeployConfig
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
        /// <summary>
        /// 应用程序白名单
        /// </summary>
        public static string[] ApplicationNameWhiteList { get; } =
        {
            "api",
            "swagger",
            "Deploy"
        };
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
            Url = Configuration["ServerInfo:Url"],
            Key = Configuration["ServerInfo:Key"],
            Name = Configuration["ServerInfo:Name"]
        };
        private static string _winRarPath;
        /// <summary>
        /// WinRar路径
        /// </summary>
        public static string WinRarPath => _winRarPath ??= Configuration["WinRarPath"];
        #endregion
    }
}
