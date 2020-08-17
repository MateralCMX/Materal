using Materal.ConvertHelper;
using Materal.DotNetty.Server.Core;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.Extensions.Configuration;
using System;

namespace Materal.ConDep.Center.Common
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
        public static ServerConfig ServerConfig => _serverConfig ??= new ServerConfig
        {
            Host = Configuration["ServerConfig:Host"],
            Port = Convert.ToInt32(Configuration["ServerConfig:Port"])
        };
        private static SqliteConfigModel _sqliteConfig;
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig => _sqliteConfig ??= new SqliteConfigModel
        {
            FilePath = Configuration["SqliteConfig:FilePath"],
            Password = Configuration["SqliteConfig:Password"],
            Version = Configuration["SqliteConfig:Version"]
        };
        private static string _defaultPassword;
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword => _defaultPassword ??= Configuration["DefaultPassword"];
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string EncodePassword(string inputString)
        {
            return $"Materal{inputString}Materal".ToMd5_32Encode();
        }
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
