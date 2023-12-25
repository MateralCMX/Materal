using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Materal.Logger
{
    /// <summary>
    /// 控制台日志记录器提供者
    /// </summary>
    [ProviderAlias("ColorConsole")]
    public sealed class ConsoleLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private MateralLoggerConfig _currentConfig;
        private readonly ConcurrentDictionary<string, ConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        public ConsoleLoggerProvider(IOptionsMonitor<MateralLoggerConfig> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }
        /// <summary>
        /// 创建日志记录器
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new ConsoleLogger(name, _currentConfig));
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
