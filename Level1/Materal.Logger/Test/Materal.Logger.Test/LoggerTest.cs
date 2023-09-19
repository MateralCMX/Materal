using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.Test
{
    [TestClass]
    public class LoggerTest
    {
        private readonly IConfiguration _configuration;
        private const string _textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}\r\n${LogID}";
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        /// <summary>
        /// 写控制台日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync()
            => await WriteLogAsync(option => option.AddConsoleTarget("ConsoleLogger", _textFormat));
        /// <summary>
        /// 写文件日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync()
            => await WriteLogAsync(option => option.AddFileTarget("FileLogger", "Logs/${Level}.log", _textFormat));
        /// <summary>
        /// 写Http日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync()
            => await WriteLogAsync(option => option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post));
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task WriteLogAsync(Action<LoggerConfigOptions> action)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger(option =>
            {
                action(option);
                option.AddAllTargetRule(LogLevel.Trace);
                option.SetLoggerLogLevel(LogLevel.Trace);
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            LoggerRuntime loggerRuntime = services.GetRequiredService<LoggerRuntime>();
            const string message = "这是一条日志消息";
            logger.LogTrace(message);
            logger.LogDebug(message);
            logger.LogInformation(message);
            logger.LogWarning(message);
            logger.LogError(message);
            logger.LogCritical(message);
            const string scopeMessage = "这是一条作用域日志消息";
            using IDisposable? scope = logger.BeginScope("CustomScope");
            logger.LogTrace(scopeMessage);
            logger.LogDebug(scopeMessage);
            logger.LogInformation(scopeMessage);
            logger.LogWarning(scopeMessage);
            logger.LogError(scopeMessage);
            logger.LogCritical(scopeMessage);
            await loggerRuntime.ShutdownAsync();
        }
    }
}