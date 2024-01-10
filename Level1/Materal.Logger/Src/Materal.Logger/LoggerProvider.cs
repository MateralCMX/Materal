namespace Materal.Logger
{
    /// <summary>
    /// 控制台日志记录器提供者
    /// </summary>
    [ProviderAlias("MateralLogger")]
    public sealed class LoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private readonly ConcurrentDictionary<string, Logger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        private readonly IOptionsMonitor<LoggerConfig> _config;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerProvider(IServiceProvider serviceProvider, IOptionsMonitor<LoggerConfig> config)
        {
            _config = config;
            _config.CurrentValue.UpdateConfig(serviceProvider).Wait();
            _config.OnChange(async m =>
            {
                await m.UpdateConfig(serviceProvider);
            });
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
        private LoggerConfig GetConfig() => _config.CurrentValue;
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
