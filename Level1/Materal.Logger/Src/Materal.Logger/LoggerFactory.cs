using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        private ILoggerProvider? _provider;
        private readonly LoggerConfig _loggerConfig;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerFactory(LoggerConfig loggerConfig, ILoggerProvider? provider = null)
        {
            _loggerConfig = loggerConfig;
            if (provider is null) return;
            AddProvider(provider);
        }
        /// <summary>
        /// 添加供应者
        /// </summary>
        /// <param name="provider"></param>
        public void AddProvider(ILoggerProvider provider) => _provider = provider;
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            ILogger logger = _provider is null ? LoggerProvider.GetNewLogger(categoryName, _loggerConfig) : _provider.CreateLogger(categoryName);
            return logger;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _provider?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
