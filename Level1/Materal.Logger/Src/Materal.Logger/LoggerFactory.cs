using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Materal.Logger
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, ILogger> _loggers = new(); 
        private ILoggerProvider? _provider;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="services"></param>
        public LoggerFactory(IServiceProvider services)
        {
            _serviceProvider = services;
            ILoggerProvider? provider = services.GetService<ILoggerProvider>();
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
            lock (_loggers)
            {
                if(_loggers.ContainsKey(categoryName)) return _loggers[categoryName];
                ILogger logger = _provider is null ? new Logger(categoryName, _serviceProvider) : _provider.CreateLogger(categoryName);
                _loggers.Add(categoryName, logger);
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
