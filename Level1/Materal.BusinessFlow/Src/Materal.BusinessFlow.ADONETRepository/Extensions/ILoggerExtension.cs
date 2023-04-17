using Microsoft.Extensions.Logging;
using System.Data;

namespace Materal.BusinessFlow.ADONETRepository.Extensions
{
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
