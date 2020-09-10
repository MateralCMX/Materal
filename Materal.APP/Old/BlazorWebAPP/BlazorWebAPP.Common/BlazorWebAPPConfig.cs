using System;
using Materal.APP.Common;
using Microsoft.Extensions.Configuration;

namespace BlazorWebAPP.Common
{
    public class BlazorWebAPPConfig
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
                if (_configuration != null) return _configuration;
                _configuration ??= ApplicationService.GetService<IConfiguration>();
                if (_configuration == null)
                {
                    ConfigurationBuilder();
                }
                return _configuration;
            }
        }
        /// <summary>
        /// 设置配置对象
        /// </summary>
        /// <param name="configuration"></param>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
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
        #endregion
        #region 配置
        /// <summary>
        /// Token
        /// </summary>
        public const string TokenKey = "Token";
        private static string _authorityUrl;
        /// <summary>
        /// 权限Url
        /// </summary>
        public static string AuthorityUrl => _authorityUrl ??= Configuration["AuthorityUrl"];
        private static string _materalAppUrl;
        /// <summary>
        /// 核心Url
        /// </summary>
        public static string MateralAppUrlUrl => _materalAppUrl ??= Configuration["MateralAppUrl"];
        #endregion
    }
}
