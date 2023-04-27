using Microsoft.Extensions.Logging;
using System.Data;

namespace Materal.TTA.ADONETRepository.Extensions
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class ILoggerExtension
    {
        /// <summary>
        /// 输出调试TSQL
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="command"></param>
        public static void LogDebugTSQL(this ILogger logger, IDbCommand command)
        {
            string message = $"T-SQL:\r\n{command.CommandText}";
            logger?.LogDebug(message);
        }
    }
}
