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
        /// ���췽��
        /// </summary>
        public LoggerTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        /// <summary>
        /// д����̨��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync()
            => await WriteLogAsync(option => option.AddConsoleTarget("ConsoleLogger", _textFormat));
        /// <summary>
        /// д�ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync()
            => await WriteLogAsync(option => option.AddFileTarget("FileLogger", "Logs/${Level}.log", _textFormat));
        /// <summary>
        /// дHttp��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync()
            => await WriteLogAsync(option => option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post));
        /// <summary>
        /// д��־
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
            const string message = "����һ����־��Ϣ";
            logger.LogTrace(message);
            logger.LogDebug(message);
            logger.LogInformation(message);
            logger.LogWarning(message);
            logger.LogError(message);
            logger.LogCritical(message);
            const string scopeMessage = "����һ����������־��Ϣ";
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