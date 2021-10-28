using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Materal.APP.Core
{
    public static class MateralAPPConfigurationBuilder
    {
        public static IConfigurationBuilder AddMateralAPPConfig(this IConfigurationBuilder config, string environmentName)
        {
            config = config.AddJsonFile("MateralAPP.json", true, true);
            string filePath = $"MateralAPP.{environmentName}.json";
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath)))
            {
                config = config.AddJsonFile(filePath, true, true);
            }
            return config;
        }
    }
}
