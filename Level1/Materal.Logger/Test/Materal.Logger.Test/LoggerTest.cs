using Materal.Logger.FileLogger;
using Materal.Utils;

namespace Materal.Logger.Test
{
    [TestClass]
    public partial class LoggerTest
    {
        /// <summary>
        /// д����̨��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync() => await WriteLogAsync(option => option.AddConsoleTarget("ConsoleLog", _textFormat, new Dictionary<LogLevel, ConsoleColor> 
        {
            [LogLevel.Trace] = ConsoleColor.DarkGray,
            [LogLevel.Debug] = ConsoleColor.DarkGreen,
            [LogLevel.Information] = ConsoleColor.Gray,
            [LogLevel.Warning] = ConsoleColor.DarkYellow,
            [LogLevel.Error] = ConsoleColor.DarkRed,
            [LogLevel.Critical] = ConsoleColor.Red
        }));
        /// <summary>
        /// д�ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddFileTarget("FileLogger", "${RootPath}\\Logs\\${Level}.log", _textFormat);
        });
        /// <summary>
        /// д������־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteBusLogTestAsync()
        {
            TestLogMonitor testLogMonitor = new();
            BusLoggerWriter.Subscribe(testLogMonitor);
            await WriteLogAsync(option => option.AddBusTarget("BusLog"));
        }
    }
    public class TestLogMonitor : ILogMonitor
    {
        public async Task OnLogAsync(Log log)
        {
            ConsoleQueue.WriteLine(log.Message);
            await Task.CompletedTask;
        }
    }
}