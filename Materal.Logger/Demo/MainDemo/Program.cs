using Materal.Common;
using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MainDemo
{
    public class Program
    {
        public static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            #region MateralLogger
            #region 通过配置文件配置
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            MateralLoggerManager.CustomConfig.Add("LogDBConnectionString", "Data Source=175.27.194.19;Database=LogTestDB; User ID=sa; Password=XMJry@456;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");
            MateralLoggerManager.CustomConfig.Add("ApplicationName", "MainDemo");
            serviceCollection.AddMateralLogger(configuration);
            #endregion
            #region 通过代码配置
            //MateralLoggerConfig.Application = "测试程序";
            //MateralLoggerConfig.ServerConfig.Enable = false;
            //MateralLoggerConfig.TargetsConfig.AddConsole("LifeConsole", 
            //    "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}|[${MachineName},${ProgressID},${ThreadID}]\r\n${Message}\r\n${Exception}",
            //    new Dictionary<LogLevel, ConsoleColor>
            //    {
            //        [LogLevel.Warning] = ConsoleColor.DarkYellow,
            //        [LogLevel.Error] = ConsoleColor.DarkRed,
            //        [LogLevel.Critical] = ConsoleColor.DarkRed,
            //    });
            //MateralLoggerConfig.RulesConfig.AddRule(new[] { "LifeConsole" });
            //serviceCollection.AddMateralLogger();
            #endregion
            #endregion
            #region Serilog
            ////Serilog不支持记录Trace和Critical
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    //.WriteTo.Console()
            //    .WriteTo.File("./Logs/Serilog.log")
            //    .WriteTo.SQLite("./Logs/SerilogDB.db")
            //    .CreateLogger();
            //serviceCollection.AddLogging(configure =>
            //{
            //    configure.ClearProviders();
            //    configure.SetMinimumLevel(LogLevel.Trace);
            //    configure.AddSerilog(dispose: true);
            //});
            #endregion
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            MateralLoggerManager.CustomData.Add("UserName", "Administrator");
            CommandBus commandBus = new();
            bool canExit = false;
            while (!canExit)
            {
                ConsoleQueue.WriteLine("请输入命令：");
                string? command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command)) continue;
                canExit = !commandBus.ExcuteCommand(command, services);
            }
        }

    }
}