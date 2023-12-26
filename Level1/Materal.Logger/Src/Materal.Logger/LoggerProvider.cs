namespace Materal.Logger
{
    /// <summary>
    /// 控制台日志记录器提供者
    /// </summary>
    [ProviderAlias("MateralLogger")]
    public sealed class LoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private LoggerConfig _currentConfig;
        private readonly ConcurrentDictionary<string, Logger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        public LoggerProvider(IOptionsMonitor<LoggerConfig> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }
        /// <summary>
        /// 创建日志记录器
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new Logger(name, GetConfig));
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        private LoggerConfig GetConfig() => _currentConfig;
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }
    }
}
