using Microsoft.Extensions.Configuration;
using System;
using Materal.ConfigurationHelper;
using Materal.ConsoleUI.Model;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        private static IConfiguration _configuration;
        public static void Main()
        {
            _configuration = ConfigurationBuilder();
            var a = _configuration.GetArrayObjectValue<ReRouteModel>("ReRoutes");
        }

        /// <summary>
        /// 配置生成
        /// </summary>
        /// <returns></returns>
        private static IConfiguration ConfigurationBuilder()
        {
            if (_configuration != null) return _configuration;
            const string appConfigFile = "ocelot.Development.json";
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
            return _configuration;
        }
    }
}
