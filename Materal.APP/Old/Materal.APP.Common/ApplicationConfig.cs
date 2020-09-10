using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;
using System;

namespace Materal.APP.Common
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
        #endregion
    }
}
