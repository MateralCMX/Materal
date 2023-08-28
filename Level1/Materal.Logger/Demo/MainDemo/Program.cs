using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainDemo
{
    public class Program
    {
        public static void Main()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("MateralLogger.json", optional: true, reloadOnChange: true)
                        .Build();
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger(configuration, options =>
            {
                options.AddCustomConfig("LogDBConnectionString", "Data Source=82.156.11.176;Database=LogTestDB; User ID=sa; Password=gdb@admin678;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");
                options.AddCustomConfig("ApplicationName", "MainDemo666");
                //const string textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}";
                //Dictionary<LogLevel, ConsoleColor> colors = new() { [LogLevel.Debug] = ConsoleColor.Blue };
                //options.AddConsoleTarget("LifeConsole", textFormat, colors);
                //options.AddFileTarget("LevelFile", "${RootPath}\\Logs\\${Date}\\${Level}.log", textFormat);
                //options.AddHttpTarget("HttpLog", "http://localhost:5000/api/Log/WriteLog", HttpMethod.Post, "{\"CreateTime\":\"${DateTime}\",\"Application\":\"${Application}\",\"Level\":\"${Level}\",\"Scope\":\"${Scope}\",\"CategoryName\":\"${CategoryName}\",\"MachineName\":\"${MachineName}\",\"ProgressID\":\"${ProgressID}\",\"ThreadID\":\"${ThreadID}\",\"Message\":\"${Message}\",\"Exception\":\"${Exception}\"}");
                //options.AddSqliteTargetFromPath("LocalDB", "${RootPath}\\Logs\\MateralLogger.db");
                //options.AddSqliteTargetFromConnectionString("LocalDB", "Data Source=${RootPath}\\Logs\\MateralLogger.db");
                //options.AddSqlServerTarget("ServerDB", "${LogDBConnectionString}");
                //options.AddWebSocketTarget("LocalWebSocket", 5002, textFormat, colors);
                //options.AddAllTargetRule();
            });
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            Random random = new();
            Console.WriteLine("按任意键开始测试");
            Console.ReadKey();
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
            //Console.WriteLine("输入完毕");
            while (true)
            {
                //logger.LogTrace($"Hello World!");
                //logger.LogDebug($"Hello World!");
                LogLevel logLevel = random.Next(0, 6) switch
                {
                    0 => LogLevel.Trace,
                    1 => LogLevel.Debug,
                    2 => LogLevel.Information,
                    3 => LogLevel.Warning,
                    4 => LogLevel.Error,
                    5 => LogLevel.Critical,
                    _ => throw new NotImplementedException()
                };
                logger.Log(logLevel, $"Hello World!");
                //logger.LogInformation($"Hello World!");
                //logger.LogWarning($"Hello World!");
                //logger.LogError($"Hello World!");
                //logger.LogCritical($"Hello World!");
                Console.ReadKey();
            }
        }
    }
}