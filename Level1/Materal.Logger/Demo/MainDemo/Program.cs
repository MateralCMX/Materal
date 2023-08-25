﻿using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainDemo
{
    public class Program
    {
        public static async Task Main()
        {
            LoggerLog.MinLevel = LogLevel.Trace;
            LoggerLog.MaxLevel = LogLevel.Critical;
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("MateralLogger.json", optional: true, reloadOnChange: true)
                        .Build();
            LoggerManager.Init(configuration);
            LoggerManager.CustomConfig.Add("LogDBConnectionString", "Data Source=82.156.11.176;Database=LogTestDB; User ID=sa; Password=gdb@admin678;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");
            LoggerManager.CustomConfig.Add("ApplicationName", "MainDemo");
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            //Random random = new();
            //for (int i = 1; i <= 10000; i++)
            //{
            //    LogLevel logLevel = random.Next(0, 6) switch
            //    {
            //        0 => LogLevel.Trace,
            //        1 => LogLevel.Debug,
            //        2 => LogLevel.Information,
            //        3 => LogLevel.Warning,
            //        4 => LogLevel.Error,
            //        5 => LogLevel.Critical,
            //        _ => throw new NotImplementedException()
            //    };
            //    logger.Log(logLevel, $"Hello World!{i}");
            //    //await Task.Delay(1000);
            //    //logger.LogTrace( $"Hello World!{i}");
            //}
            logger.LogTrace($"Hello World!");
            logger.LogDebug($"Hello World!");
            logger.LogInformation($"Hello World!");
            logger.LogWarning($"Hello World!");
            logger.LogError($"Hello World!");
            logger.LogCritical($"Hello World!");
            Console.WriteLine("输入完毕");
            Console.ReadKey();
        }
    }
}