using Materal.ConfigurationHelper;
using Microsoft.Extensions.Configuration;
using System;
using Materal.TTA.Common.Model;

namespace Demo.Common
{
    public static class DemoConfig
    {
        #region 配置对象
        private static IConfiguration _configuration;
        public const string AppName = "Materal.TTA.Demo";
        private const string DefaultConfigFileName = "DemoConfig";
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
        #region 配置
        private static SqlServerConfigModel _demoDBConfig;
        /// <summary>
        /// DemoDB配置
        /// </summary>
        public static SqlServerConfigModel DemoDBConfig => _demoDBConfig ?? (_demoDBConfig = new SqlServerConfigModel
        {
            Address = Configuration["SQLServerDB:DemoDB:Address"],
            Port = Configuration["SQLServerDB:DemoDB:Port"],
            Name = Configuration["SQLServerDB:DemoDB:Name"],
            UserID = Configuration["SQLServerDB:DemoDB:UserID"],
            Password = Configuration["SQLServerDB:DemoDB:Password"],
            SubordinateConfigs = Configuration.GetArrayObjectValue<SqlServerSubordinateConfigModel>("SQLServerDB:DemoDB:SubordinateDB")
        });
        #endregion
    }
}
