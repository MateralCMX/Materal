using System.Data;

namespace Materal.TTA.Common
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class ILoggerExtension
    {
        /// <summary>
        /// TSQL日志等级
        /// </summary>
        public static LogLevel TSQLLogLevel { get; set; } = LogLevel.Debug;
        /// <summary>
        /// 启用
        /// </summary>
        public static bool Enable { get; set; } = true;
        /// <summary>
        /// 输出调试TSQL
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="command"></param>
        public static void LogTSQL(this ILogger logger, string command)
        {
            string message = $"T-SQL:\r\n{command}";
            logger?.Log(TSQLLogLevel, message);
        }
        /// <summary>
        /// 输出调试TSQL
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="command"></param>
        public static void LogTSQL(this ILogger logger, IDbCommand command)
        {
            string message = $"T-SQL:\r\n{command.CommandText}";
            logger?.Log(TSQLLogLevel, message);
        }
    }
}
