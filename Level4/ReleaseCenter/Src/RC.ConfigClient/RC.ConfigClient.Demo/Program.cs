﻿using Materal.ConfigurationHelper;
using Microsoft.Extensions.Configuration;

namespace RC.ConfigClient.Demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddDefaultNameSpace("http://175.27.254.187:8700/RCES_FatAPI", "MateralReleaseCenter", 10)
                .AddNameSpace("ConfigClient");
            IConfiguration _configuration = configurationBuilder.Build();
            while (true)
            {
                string? testValue = _configuration.GetValue("Test");
                Console.WriteLine(testValue);
                await Task.Delay(1000);
            }
        }
    }
}