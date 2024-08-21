namespace Materal.Logger
{
    /// <summary>
    /// 日志提供者
    /// </summary>
    [ProviderAlias("MateralLogger")]
    public class LoggerProvider : ILoggerProvider
    {
        private readonly IOptionsMonitor<LoggerOptions> _options;
        private readonly IDisposable? _optionsReloadToken;
        private readonly ConcurrentDictionary<string, Logger> _loggers = [];
        private readonly ILoggerHost _loggerHost;
        private readonly ILoggerListener _loggerListener;
        /// <summary>
        /// 构造函数
        /// </summary>
        public LoggerProvider(IOptionsMonitor<LoggerOptions> options, ILoggerHost loggerHost, ILoggerListener loggerListener, IConfiguration? configuration = null)
        {
            _options = options;
            _options.CurrentValue.Configuration = configuration;
            _loggerHost = loggerHost;
            _loggerListener = loggerListener;
            ReloadLoggerOptions(_options.CurrentValue);
            _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            _loggerHost.StartAsync().Wait();
        }
        private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
            => _loggerHost.StopAsync().Wait();
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            if (!_loggers.TryGetValue(categoryName, out Logger? value))
            {
                value = _loggers.GetOrAdd(categoryName, new Logger(categoryName, _options.CurrentValue, _loggerHost, _loggerListener));
            }
            return value;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _loggers.Clear();
            _optionsReloadToken?.Dispose();
            _loggerListener?.Dispose();
        }
        /// <summary>
        /// 重新加载日志配置
        /// </summary>
        /// <param name="options"></param>
        private void ReloadLoggerOptions(LoggerOptions options)
        {
            options.UpdateTargetOptions();
            _loggerHost.Options = options;
            foreach (KeyValuePair<string, Logger> logger in _loggers)
            {
                logger.Value.Options = options;
            }
        }
    }
}
