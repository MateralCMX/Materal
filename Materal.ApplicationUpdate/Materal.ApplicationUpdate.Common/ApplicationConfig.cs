using Microsoft.Extensions.Configuration;
using Materal.StringHelper;
using System;

namespace Materal.ApplicationUpdate.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public static class ApplicationConfig
    {
        /// <summary>
        /// MD5盐值
        /// </summary>
        public static string MD5Salt => Configuration["MD5Salt"];
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword => Configuration["UserDomain:DefaultPassword"];
        /// <summary>
        /// Token有效秒数
        /// </summary>
        public static int TokenEffectiveSecond
        {
            get
            {
                string configStr = Configuration["UserDomain:TokenEffectiveSecond"];
                if (!string.IsNullOrEmpty(configStr) && configStr.IsIntegerPositive())
                {
                    return Convert.ToInt32(configStr);
                }
                return 3600;
            }
        }
        /// <summary>
        /// Token刷新
        /// </summary>
        public static bool TokenRefresh
        {
            get
            {
                string configStr = Configuration["UserDomain:TokenRefresh"];
                return !string.IsNullOrEmpty(configStr) && Convert.ToBoolean(configStr);
            }
        }
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
