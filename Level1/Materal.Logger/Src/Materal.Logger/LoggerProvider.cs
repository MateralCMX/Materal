using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志供应者
    /// </summary>
    public class LoggerProvider : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
            => GetNewLogger(categoryName, _serviceProvider);
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);
        /// <summary>
        /// 获得新的日志
        /// </summary>
        /// <returns></returns>
        public static Logger GetNewLogger(string categoryName, IServiceProvider serviceProvider) => new(categoryName, serviceProvider);
    }
}
