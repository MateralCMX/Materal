using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Threading.Tasks;

#pragma warning disable CA2017 // 参数计数不匹配。
namespace Materal.Logger.Test
{
    public partial class LoggerTest
    {
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
        /// <param name="logger"></param>
        private static void WriteLogs(IServiceProvider services)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            WriteLogs(logger, "这是一条日志消息");
            using (IDisposable scope = logger.BeginScope("CustomScope"))
            {
                WriteLogs(logger, "这是一条作用域日志消息");
            }
            using (IDisposable scope = logger.BeginScope(new AdvancedScope("AdvancedScope", new Dictionary<string, string>
            {
                ["UserID"] = Guid.NewGuid().ToString()
            })))
            {
                WriteLogs(logger, "这是一条高级作用域日志消息[${UserID}]");
            }
        }
        /// <summary>
        /// 写大量日志
        /// </summary>
        /// <param name="logger"></param>
        private static void WriteLargeLogs(IServiceProvider services, LogLevel logLevel, int count = 10000)
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
        private static void WriteThreadLogs(IServiceProvider services, int threadCount = 100)
        {
            ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
            List<Task> tasks = new();
            for (int i = 0; i < threadCount; i++)
            {
                string index = i.ToString();
                Task task = Task.Run(() =>
                {
                    ILogger logger = loggerFactory.CreateLogger("NewThread");
                    WriteLogs(logger, "这是一条多线程日志消息");
                });
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
#pragma warning restore CA2017 // 参数计数不匹配。
