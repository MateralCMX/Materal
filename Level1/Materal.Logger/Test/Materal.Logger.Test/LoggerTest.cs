namespace Materal.Logger.Test
{
    [TestClass]
    public class LoggerTest
    {
        private readonly IConfiguration _configuration;
        private const string _textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// ���췽��
        /// </summary>
        public LoggerTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("MateralLogger.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        /// <summary>
        /// д����̨��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void WriteConsoleLogTest() => WriteLog(option =>
        {
            option.AddConsoleTarget("ConsoleLogger", _textFormat);
        });
        #region ��ݷ���
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private void WriteLog(Action<LoggerOptions>? action, bool loadConfigFile = false) => WriteLog(action, services =>
        {
            WriteLogs(services, 10);
            WriteThreadLogs(services, 100);
            WriteLargeLogs(services, LogLevel.Information, 10000);
        }, loadConfigFile);
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private void WriteLog(Action<LoggerOptions>? action, Action<IServiceProvider> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = Init(action, loadConfigFile);
            testFunc.Invoke(services);
        }
        /// <summary>
        /// д��־
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
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="action"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        private IServiceProvider Init(Action<LoggerOptions>? action, bool loadConfigFile = false)
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
                builder.Services.Configure<LoggerOptions>(option =>
                {
                    action?.Invoke(option);
                    if (!loadConfigFile)
                    {
                        //option.AddAllTargetsRule();
                    }
                });
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            return services;
        }
        /// <summary>
        /// д��־
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
        /// д��־
        /// </summary>
        /// <param name="message"></param>
        private static void WriteLog(IServiceProvider services, LogLevel logLevel, string message)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            logger.Log(logLevel, message);
        }
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="count"></param>
        public static void WriteLogs(IServiceProvider services, int count = 1)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 0; i < count; i++)
            {
                WriteLogs(logger, $"{i}����һ����־��Ϣ");
            }
            using (IDisposable? scope = logger.BeginScope("CustomScope"))
            {
                for (int i = 0; i < count; i++)
                {
                    WriteLogs(logger, $"{i}����һ����־��Ϣ");
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
                    WriteLogs(logger, $"{i}����һ���߼���������־��Ϣ[${{UserID}}]");
                }
            }
        }
        /// <summary>
        /// дѭ����־
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
                WriteLogs(logger, $"����һ��ѭ����־[{i}]");
            }
        }
        /// <summary>
        /// д������־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logLevel"></param>
        /// <param name="count"></param>
        public static void WriteLargeLogs(IServiceProvider services, LogLevel logLevel = LogLevel.Information, int count = 10000)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 1; i <= count; i++)
            {
                logger.Log(logLevel, $"[{i:00000}]����һ��������¼����־");
            }
        }
        /// <summary>
        /// д���߳���־
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
                        WriteLogs(logger, $"[Thread{index}][{i}]����һ�����߳���־��Ϣ");
                    }
                });
                tasks.Add(task);
            }
            Task.WaitAll([.. tasks]);
        }
        #endregion
    }
}