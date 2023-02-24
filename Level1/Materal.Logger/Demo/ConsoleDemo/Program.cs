using Materal.Logger;
using Materal.Utils;
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
            LoggerManager.CustomData.Add("UserName", "Administrator");
            #region 配置文件方式
            LoggerManager.CustomConfig.Add("ApplicationName", "ConsoleDemo");//会替换配置文件中${{ApplicationName}}的地方
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            LoggerManager.Init(null, configuration);
            #endregion
            #region 代码配置方式
            //MateralLoggerManager.Init(option => {
            //    const string messageFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}";
            //    option.AddConsoleTarget("LifeConsole", messageFormat);
            //    //option.AddFileTarget("LevelFile", "${RootPath}\\Logs\\${Date}\\${Level}.log", _messageFormat);
            //    //option.AddSqliteTarget("LocalDB", "${RootPath}\\Logs\\Logger.db");
            //    option.AddAllTargetRule();
            //});
            #endregion
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