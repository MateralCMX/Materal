using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志自身日志
    /// </summary>
    public interface ILoggerLog
    {
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLevel { get; }
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLevel { get; }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        Task ShutdownAsync();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray);
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        void LogInfomation(string message);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void LogWarning(string message, Exception? exception);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void LogError(string message, Exception? exception);
    }
}
