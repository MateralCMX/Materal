namespace Materal.Logger.Test
{
    public partial class LoggerTest
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
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerOptions>? action, bool loadConfigFile = false) => await WriteLogAsync(action, services =>
        {
            WriteLogs(services, 10);
            WriteThreadLogs(services, 100);
            WriteLargeLogs(services, 10000);
        }, loadConfigFile);
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerOptions>? action, Action<IServiceProvider> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = Init(action, loadConfigFile);
            testFunc.Invoke(services);
            ILoggerHost loggerHost = services.GetRequiredService<ILoggerHost>();
            await loggerHost.StopAsync();
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerOptions>? action, Func<IServiceProvider, Task> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = Init(action, loadConfigFile);
            await testFunc.Invoke(services);
            ILoggerHost loggerHost = services.GetRequiredService<ILoggerHost>();
            await loggerHost.StopAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        private IServiceProvider Init(Action<LoggerOptions>? action, bool loadConfigFile = false)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            if (loadConfigFile)
            {
                serviceCollection.AddMateralLogger(_configuration);
            }
            else
            {
                serviceCollection.AddMateralLogger();
            }
            serviceCollection.Configure<LoggerOptions>(option =>
            {
                action?.Invoke(option);
                if (!loadConfigFile)
                {
                    option.AddAllTargetsRule();
                }
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
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
        /// <param name="message"></param>
        private static void WriteLog(IServiceProvider services, LogLevel logLevel, string message)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            logger.Log(logLevel, message);
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
            LoggerScope loggerScope = new("LoggerScopeTest", new Dictionary<string, object?>
            {
                ["UserID"] = Guid.NewGuid()
            });
            using (IDisposable? scope = logger.BeginScope(loggerScope))
            {
                for (int i = 0; i < count; i++)
                {
                    loggerScope.ScopeData["CustomData"] = Guid.NewGuid();
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
        /// <param name="count"></param>
        public static void WriteLargeLogs(IServiceProvider services, int count = 10000)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            Random random = new();
            for (int i = 1; i <= count; i++)
            {
                LogLevel logLevel = random.Next(0, 6) switch
                {
                    0 => LogLevel.Trace,
                    1 => LogLevel.Debug,
                    2 => LogLevel.Information,
                    3 => LogLevel.Warning,
                    4 => LogLevel.Error,
                    5 => LogLevel.Critical,
                    _ => LogLevel.None
                };
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
