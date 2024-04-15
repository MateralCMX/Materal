using Materal.Utils;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleTest
{
    public static class SendLoggerManager
    {
        private const string _scopeName = "CustomScope";
        public static void Send(ILogger logger)
        {
            string? inputArg = null;
            while (inputArg is null || string.IsNullOrWhiteSpace(inputArg) || !inputArg.IsNumber())
            {
                ConsoleQueue.WriteLine("1:发送一次   2:发送自定义域一次  3:发送高级域一次");
                ConsoleQueue.WriteLine("4:循环发送   5:循环发送自定义域  6:循环发送高级域");
                ConsoleQueue.WriteLine("请选择测试类型：");
                inputArg = Console.ReadLine();
            }
            int mode = int.Parse(inputArg);
            switch (mode)
            {
                case 1:
                    SendOne(logger);
                    break;
                case 2:
                    SendOneByScopeName(logger);
                    break;
                case 3:
                    SendOneByAdvancedScope(logger);
                    break;
                case 4:
                    SendLoop(logger);
                    break;
                case 5:
                    SendLoopByScopeName(logger);
                    break;
                case 6:
                    SendLoopByAdvancedScope(logger);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 发送一次性日志
        /// </summary>
        /// <param name="logger"></param>
        public static void SendOne(ILogger logger)
        {
#pragma warning disable CA2017 // 参数计数不匹配。
            logger.LogTrace("Trace[${UserID}][${CustomMessage}]");
            logger.LogDebug("Debug[${UserID}][${CustomMessage}]");
            logger.LogInformation("Information[${UserID}][${CustomMessage}]");
            logger.LogWarning("Warning[${UserID}][${CustomMessage}]");
            logger.LogError("Error[${UserID}][${CustomMessage}]");
            logger.LogCritical("Critical[${UserID}][${CustomMessage}]");
#pragma warning restore CA2017 // 参数计数不匹配。
        }
        /// <summary>
        /// 发送一次性日志
        /// </summary>
        /// <param name="logger"></param>
        public static void SendOneByScopeName(ILogger logger)
        {
            using IDisposable? _scope = logger.BeginScope(_scopeName);
            SendOne(logger);
        }
        /// <summary>
        /// 发送一次性日志
        /// </summary>
        /// <param name="logger"></param>
        public static void SendOneByAdvancedScope(ILogger logger)
        {
            using IDisposable? scope = logger.BeginScope(new AdvancedScope(_scopeName, new()
            {
                ["UserID"] = Guid.NewGuid(),
                ["CustomMessage"] = "Hello World!"
            }));
            SendOne(logger);
        }
        /// <summary>
        /// 发送循环日志
        /// </summary>
        /// <param name="logger"></param>
        public static void SendLoop(ILogger logger)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (true)
                {
                    SendOne(logger);
                    Thread.Sleep(1000);
                }
            });
        }
        /// <summary>
        /// 发送循环日志
        /// </summary>
        /// <param name="logger"></param>
        public static void SendLoopByScopeName(ILogger logger)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using IDisposable? scope = logger.BeginScope(_scopeName);
                while (true)
                {
                    SendOne(logger);
                    Thread.Sleep(1000);
                }
            });
        }
        /// <summary>
        /// 发送循环日志
        /// </summary>
        /// <param name="logger"></param>
        public static void SendLoopByAdvancedScope(ILogger logger)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using IDisposable? scope = logger.BeginScope(new AdvancedScope(_scopeName, new()
                {
                    ["UserID"] = Guid.NewGuid(),
                    ["CustomMessage"] = "Hello World!"
                }));
                while (true)
                {
                    SendOne(logger);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
