using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志供应者
    /// </summary>
    public class LoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) 
            => new Logger(categoryName);
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
