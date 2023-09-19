using Materal.Utils;
using Materal.Utils.Http;
using Materal.Utils.Model;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志对象日志
    /// </summary>
    public class LoggerLog
    {
        private readonly ActionBlock<ConsoleMessageModel> _writeBuffer;
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLevel => _loggerConfig.LoggerLogLevel.MinLevel;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLevel => _loggerConfig.LoggerLogLevel.MaxLevel;
        private readonly LoggerConfig _loggerConfig;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="loggerConfig"></param>
        public LoggerLog(LoggerConfig loggerConfig)
        {
            _writeBuffer = new(WriteMessage);
            _loggerConfig = loggerConfig;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public async Task ShutdownAsync()
        {
            _writeBuffer.Complete();
            await _writeBuffer.Completion;
        }
        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="model"></param>
        private void WriteMessage(ConsoleMessageModel model)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = model.Color;
            if (model.Args != null)
            {
                Console.WriteLine(model.Message, model.Args);
            }
            else
            {
                Console.WriteLine(model.Message);
            }
            Console.ForegroundColor = foregroundColor;
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        public void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (logLevel < MinLevel || logLevel > MaxLevel) return;
            WriteMessage(new()
            {
                Message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|LoggerLog|{logLevel}|{message}",
                Color = consoleColor
            });
        }
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message) => Log(message, LogLevel.Debug, ConsoleColor.DarkGreen);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        public void LogInfomation(string message) => Log(message, LogLevel.Information, ConsoleColor.Gray);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message) => Log(message, LogLevel.Warning, ConsoleColor.DarkYellow);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void LogWarning(string message, Exception? exception) => LogWarning(GetErrorMessage(message, exception));
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message) => Log(message, LogLevel.Error, ConsoleColor.DarkRed);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void LogError(string message, Exception? exception) => LogError(GetErrorMessage(message, exception));
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetErrorMessage(string message, Exception? exception)
        {
            StringBuilder messageBuild = new();
            messageBuild.AppendLine(message);
            if (exception is not null)
            {
                switch (exception)
                {
                    case MateralHttpException httpException:
                        messageBuild.AppendLine(httpException.GetExceptionMessage());
                        break;
                    default:
                        messageBuild.AppendLine(exception.GetErrorMessage());
                        break;
                }
            }
            return messageBuild.ToString();
        }
    }
}
