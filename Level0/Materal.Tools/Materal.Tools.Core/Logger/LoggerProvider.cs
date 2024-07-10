using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 日志提供者
    /// </summary>
    [ProviderAlias("MateralLogger")]
    public class LoggerProvider(ILoggerListener loggerListener) : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, Logger> _loggers = [];
        /// <summary>
        /// 创建记录器
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            if (!_loggers.TryGetValue(categoryName, out Logger? value))
            {
                value = _loggers.GetOrAdd(categoryName, new Logger(categoryName, loggerListener));
            }
            return value;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _loggers.Clear();
            loggerListener?.Dispose();
        }
    }
}
