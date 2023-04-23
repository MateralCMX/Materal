using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public class LoggerFactory : ILoggerFactory, IDisposable
    {
        private readonly Dictionary<string, ILogger> _loggers = new(); 
        private ILoggerProvider? _provider;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="services"></param>
        public LoggerFactory(IServiceProvider services)
        {
            ILoggerProvider? provider = services.GetService<ILoggerProvider>();
            if(provider != null)
            {
                AddProvider(provider);
            }
        }
        /// <summary>
        /// 添加供应者
        /// </summary>
        /// <param name="provider"></param>
        public void AddProvider(ILoggerProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            lock (_loggers)
            {
                if(_provider == null) return new Logger(categoryName);
                if (!_loggers.TryGetValue(categoryName, out var logger))
                {
                    logger = _provider.CreateLogger(categoryName);
                    _loggers[categoryName] = logger;
                }
                return logger;
            }
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
