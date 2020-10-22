using Materal.APP.Core.Models;
using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Materal.APP.Core
{
    public static class ApplicationConfig
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
            const string appConfigFile = "MateralAPPConfig.Development.json";
#else
            const string appConfigFile = "MateralAPPConfig.json";
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
        /// Des加密Key
        /// </summary>
        public const string DesKey = "MateralM";
        /// <summary>
        /// Des加密向量
        /// </summary>
        public const string DesIV = "MateralV";
        private static JWTConfigModel _jwtConfig;
        /// <summary>
        /// JWT配置
        /// </summary>
        public static JWTConfigModel JWTConfig => _jwtConfig ??= new JWTConfigModel
        {
            Key = Configuration["JWT:Key"],
            Audience = Configuration["JWT:Audience"],
            Issuer = Configuration["JWT:Issuer"],
            ExpiredTime = Configuration["JWT:ExpiredTime"].ConvertTo<uint>()
        };

        private static bool? _showException;
        /// <summary>
        /// 显示异常
        /// </summary>
        public static bool ShowException
        {
            get
            {
                _showException ??= Configuration["ShowException"].ConvertTo<bool>();
                return _showException.Value;
            }
        }

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
        /// 链接地址
        /// </summary>
        public static string Url{ get; set; }
        /// <summary>
        /// 公开链接地址
        /// </summary>
        public static string PublicUrl { get; set; }
        /// <summary>
        /// 获得程序版本号
        /// </summary>
        /// <returns></returns>
        public static string GetProgramVersion()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null) return "未知版本";
            Version version = entryAssembly.GetName().Version;
            return version == null ? "未知版本" : version.ToString();
        }
        #endregion
    }
}
