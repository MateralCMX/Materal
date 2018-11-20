using Microsoft.Extensions.Configuration;
using System;

namespace Materal.ApplicationUpdate.Common
{
    public static class ApplicationConfig
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static ApplicationConfig()
        {
            Reload();
        }
        /// <summary>
        /// 重新加载配置文件
        /// </summary>
        public static void Reload()
        {
#if DEBUG
            const string configFileName = "appsettings.Development.json";
#else
            const string configFileName = "appsettings.json";
#endif
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(configFileName);
            Configuration = builder.Build();
        }
    }
}
