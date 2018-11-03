using Microsoft.Extensions.Configuration;
using System;

namespace Materal.WPFUI
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
        /// 配置文件Builder
        /// </summary>
        /// <returns></returns>
        public static IConfiguration ConfigurationBuilder()
        {
            if (_configuration != null) return _configuration;
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration;
        }
    }
}
