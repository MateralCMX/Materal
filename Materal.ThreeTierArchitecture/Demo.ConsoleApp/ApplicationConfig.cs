using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Materal.ConvertHelper;
using Materal.TTA.SqlServerRepository.Model;
using Microsoft.Extensions.Configuration;

namespace Demo.ConsoleApp
{
    public static class ApplicationConfig
    {
        #region 配置对象
        private static IConfiguration _configuration;
        private const string DefaultConfigFileName = "appsetting";
        private const string DefaultConfigFileSuffix = "json";
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
        /// <param name="configTarget">配置目标</param>
        /// <returns></returns>
        public static void ConfigurationBuilder(string configTarget = null)
        {
            string appConfigFile = string.IsNullOrEmpty(configTarget) ?
                $"{DefaultConfigFileName}.{DefaultConfigFileSuffix}"
                : $"{DefaultConfigFileName}.{configTarget}.{DefaultConfigFileSuffix}";
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
        }
        #endregion

        #region 配置项
        private static SqlServerConfigModel _userDB;

        /// <summary>
        /// 用户数据库
        /// </summary>
        public static SqlServerConfigModel UserDB
        {
            get
            {
                if (_userDB == null)
                {
                    _userDB = new SqlServerConfigModel
                    {
                        Address = Configuration["UserDB:Address"],
                        Port = Configuration["UserDB:Port"],
                        Name = Configuration["UserDB:Name"],
                        UserID = Configuration["UserDB:UserID"],
                        Password = Configuration["UserDB:Password"]
                    };
                    IConfigurationSection subordinateConfigs = Configuration.GetSection("UserDB:SubordinateConfigs");
                    _userDB.SubordinateConfigs = subordinateConfigs.GetChildren().Select(m => new SqlServerSubordinateConfigModel
                    {
                        Address = m.GetSection("Address").Value,
                        Port = m.GetSection("Port").Value,
                        Name = m.GetSection("Name").Value,
                        UserID = m.GetSection("UserID").Value,
                        Password = m.GetSection("Password").Value
                    }).ToList();
                }
                return _userDB;
            }
        }
        #endregion
    }
}
