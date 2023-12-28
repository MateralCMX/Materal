using Materal.Logger.ConfigModels;
using Materal.Logger.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.Test
{
    [TestClass]
    public class LoggerTest
    {
        private readonly IConfiguration _configuration;
        private const string _textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("MateralLogger.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        ///// <summary>
        ///// 写TraceListener日志
        ///// </summary>
        ///// <returns></returns>
        //[TestMethod]
        //public async Task WriteTraceListenerLogTestAsync()
        //{
        //    IServiceCollection serviceCollection = new ServiceCollection();
        //    serviceCollection.AddLogging(builder =>
        //    {
        //        builder.AddMateralLogger();
        //    });
        //    IServiceProvider services = serviceCollection.BuildServiceProvider();
        //    await services.UseMateralLoggerAsync();
        //    WriteLogs(services);
        //    Trace.WriteLine("[Trace]Hello World!");
        //    Debug.WriteLine("[Debug]Hello World!");
        //}
        /// <summary>
        /// 写控制台日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddConsoleTarget("ConsoleLogger", _textFormat);
        });
        /// <summary>
        /// 写文件日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddFileTarget("FileLogger", "${RootPath}\\Logs\\${Level}.log", _textFormat);
        });
        /// <summary>
        /// 写Http日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post);
        });
        /// <summary>
        /// 写Sqlite日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync() => await WriteLogAsync(option => 
        { 
            option.AddSqliteTargetFromPath("SqliteLogger", "${RootPath}\\Logs\\MateralLogger.db", "${Level}Log"); 
        });
        /// <summary>
        /// 写SqlServer日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqlServerLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Data Source=127.0.0.1;Database=MateralLoggerTestDB; User ID=sa; Password=Materal@1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
            option.AddSqlServerTarget("SqlServerLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// 写Mongo日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMongoLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "mongodb://localhost:27017/";
            option.AddMongoTarget("MongoLogger", connectionString);
        });
        /// <summary>
        /// 写WebSocket日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteWebSocketLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddWebSocketTarget("WebSocketLogger", 5002);
        }, async services => await WriteLoopLogsAsync(services, 10, 1000));
        /// <summary>
        /// 写配置文件日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConfigFileLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddCustomConfig("ApplicationName", "DyLoggerTest");
        }, true);
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfig>? action, bool loadConfigFile = false) => await WriteLogAsync(action, services =>
        {
            WriteLogs(services, 10);
            WriteThreadLogs(services, 100);
            WriteLargeLogs(services, LogLevel.Information, 10000);
        }, loadConfigFile);
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfig>? action, Action<IServiceProvider> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = await Init(action, loadConfigFile);
            testFunc.Invoke(services);
            await LoggerHost.ShutdownAsync();
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfig>? action, Func<IServiceProvider, Task> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = await Init(action, loadConfigFile);
            await testFunc.Invoke(services);
            await LoggerHost.ShutdownAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        private async Task<IServiceProvider> Init(Action<LoggerConfig>? action, bool loadConfigFile = false)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                if (loadConfigFile)
                {
                    builder.AddMateralLogger(_configuration);
                }
                else
                {
                    builder.AddMateralLogger();
                }
                builder.AddMateralLoggerConfig(option =>
                {
                    action?.Invoke(option);
                    if (!loadConfigFile)
                    {
                        option.AddAllTargetsRule();
                    }
                });
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            await services.UseMateralLoggerAsync();
            return services;
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        private static void WriteLogs(ILogger logger, string message)
        {
            logger.LogTrace(message);
            logger.LogDebug(message);
            logger.LogInformation(message);
            logger.LogWarning(message);
            logger.LogError(message);
            logger.LogCritical(message);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="count"></param>
        public static void WriteLogs(IServiceProvider services, int count = 1)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 0; i < count; i++)
            {
                WriteLogs(logger, $"{i}这是一条日志消息");
            }
            using (IDisposable? scope = logger.BeginScope("CustomScope"))
            {
                for (int i = 0; i < count; i++)
                {
                    WriteLogs(logger, $"{i}这是一条日志消息");
                }
            }
            AdvancedScope advancedScope = new("AdvancedScope", new Dictionary<string, object?>
            {
                ["UserID"] = Guid.NewGuid()
            });
            using (IDisposable? scope = logger.BeginScope(advancedScope))
            {
                for (int i = 0; i < count; i++)
                {
                    advancedScope.ScopeData["CustomData"] = Guid.NewGuid();
                    WriteLogs(logger, $"{i}这是一条高级作用域日志消息[${{UserID}}]");
                }
            }
        }
        /// <summary>
        /// 写循环日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loopCount"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static async Task WriteLoopLogsAsync(IServiceProvider services, int loopCount = 10, int interval = 1000)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 0; i < loopCount; i++)
            {
                await Task.Delay(interval);
                WriteLogs(logger, $"这是一条循环日志[{i}]");
            }
        }
        /// <summary>
        /// 写大量日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logLevel"></param>
        /// <param name="count"></param>
        public static void WriteLargeLogs(IServiceProvider services, LogLevel logLevel = LogLevel.Information, int count = 10000)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 1; i <= count; i++)
            {
                logger.Log(logLevel, $"[{i:00000}]这是一条大量记录的日志");
            }
        }
        /// <summary>
        /// 写多线程日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="threadCount"></param>
        public static void WriteThreadLogs(IServiceProvider services, int threadCount = 100)
        {
            ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
            List<Task> tasks = [];
            for (int i = 0; i < threadCount; i++)
            {
                string index = i.ToString();
                Task task = Task.Run(() =>
                {
                    ILogger logger = loggerFactory.CreateLogger($"Thread{index}");
                    for (int i = 0; i < 10; i++)
                    {
                        WriteLogs(logger, $"[Thread{index}][{i}]这是一条多线程日志消息");
                    }
                });
                tasks.Add(task);
            }
            Task.WaitAll([.. tasks]);
        }
    }
}