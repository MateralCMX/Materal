using Materal.Common;
using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleDemo
{
    public class Program
    {
        public static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger();
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            MateralLoggerManager.CustomData.Add("UserName", "Administrator");
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            MateralLoggerManager.Init(null, configuration);
            //MateralLoggerManager.Init(option => {
            //    const string messageFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}";
            //    option.AddConsoleTarget("LifeConsole", messageFormat);
            //    //option.AddFileTarget("LevelFile", "${RootPath}\\Logs\\${Date}\\${Level}.log", _messageFormat);
            //    //option.AddSqliteTarget("LocalDB", "${RootPath}\\Logs\\Logger.db");
            //    option.AddAllTargetRule();
            //});
            ILogger<Program> logger = services.GetService<ILogger<Program>>() ?? throw new Exception("获取日志记录器失败");
            ConsoleQueue.WriteLine("按任意键进行日志输出");
            Console.ReadKey();
            const string message = "这是一条日志消息";
            logger.LogTrace(message);
            logger.LogDebug(message);
            logger.LogInformation(message);
            logger.LogWarning(message);
            logger.LogError(message);
            logger.LogCritical(message);
            using IDisposable? scope = logger.BeginScope("ConsoleScope");
            logger.LogTrace(message);
            logger.LogDebug(message);
            logger.LogInformation(message);
            logger.LogWarning(message);
            logger.LogError(message);
            logger.LogCritical(message);
            ConsoleQueue.WriteLine("按任意键退出程序");
            Console.ReadKey();
        }

    }
}