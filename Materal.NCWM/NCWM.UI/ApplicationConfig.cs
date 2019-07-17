using Materal.ConfigurationHelper;
using Microsoft.Extensions.Configuration;
using NCWM.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace NCWM.UI
{
    public static class ApplicationConfig
    {
        /// <summary>
        /// 配置文件对象
        /// </summary>
        private static IConfiguration _configuration;
        /// <summary>
        /// 配置文件对象
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration != null) return _configuration;
                _configuration = ConfigurationBuilder();
                return _configuration;
            }
            set => _configuration = value;
        }
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        /// <summary>
        /// 配置文件Builder
        /// </summary>
        /// <returns></returns>
        private static IConfiguration ConfigurationBuilder()
        {
            if (_configuration != null) return _configuration;
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration;
        }
        /// <summary>
        /// Configs
        /// </summary>
        private static List<ConfigModel> _configs;

        /// <summary>
        /// Configs
        /// </summary>
        public static List<ConfigModel> Configs
        {
            get => _configs ?? (_configs = Configuration.GetArrayObjectValue<ConfigModel>("Configs"));
            set => _configs = value;
        }

        private static TitleConfig _titleConfig;
        /// <summary>
        /// 标题
        /// </summary>
        public static TitleConfig TitleConfig
        {
            get => _titleConfig ?? (_titleConfig = new TitleConfig
            {
                Text = Configuration["Title:Text"] ?? "Materal .NetCore启动器",
                DisplayVersion = Convert.ToBoolean(Configuration["Title:DisplayVersion"] ?? "true")
            });
            set => _titleConfig = value;
        }
    }
}
