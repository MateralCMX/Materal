using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private ILoggerProvider? _provider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ILoggerProvider? provider = _serviceProvider.GetService<ILoggerProvider>();
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
            ILogger logger = _provider is null ? LoggerProvider.GetNewLogger(categoryName, _serviceProvider) : _provider.CreateLogger(categoryName);
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
