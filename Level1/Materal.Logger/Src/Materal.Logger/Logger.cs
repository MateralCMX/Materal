using System.Collections;

namespace Materal.Logger
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public class Logger(string categoryName, Func<LoggerConfig> getConfig) : ILogger
    {
        private LoggerScope? _loggerScope;
        /// <summary>
        /// 开启作用域
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            const string defaultScope = "PublicScope";
            if (state is null)
            {
                _loggerScope = BeginScope(defaultScope);
            }
            else if (state is not null)
            {
                if (state is string stateString)
                {
                    _loggerScope = BeginScope(stateString);
                }
                else if (state is AdvancedScope advancedScope)
                {
                    _loggerScope = BeginScope(advancedScope);
                }
                else if (state is Dictionary<string, object?> objDictionary)
                {
                    _loggerScope = BeginScope(new AdvancedScope(defaultScope, objDictionary));
                }
                else if (state is not IEnumerable collection)
                {
                    Type stateType = state.GetType();
                    if (stateType.IsClass)
                    {
                        try
                        {
                            PropertyInfo[] propertyInfos = state.GetType().GetProperties();
                            Dictionary<string, object?> scopeData = [];
                            foreach (PropertyInfo propertyInfo in propertyInfos)
                            {
                                if (!propertyInfo.CanRead) continue;
                                object? valueObj = propertyInfo.GetValue(state);
                                scopeData.TryAdd(propertyInfo.Name, valueObj);
                            }
                            _loggerScope = BeginScope(new AdvancedScope(defaultScope, scopeData));
                        }
                        catch
                        {
                            _loggerScope = null;
                        }
                    }
                }
                _loggerScope ??= BeginScope(state.ToString() ?? defaultScope);
            }
            return _loggerScope;
        }
        /// <summary>
        /// 开启作用域
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public LoggerScope BeginScope(AdvancedScope scope)
        {
            if (_loggerScope is null)
            {
                _loggerScope = new LoggerScope(scope, this);
            }
            else if (_loggerScope.ScopeName != scope.ScopeName)
            {
                _loggerScope.Dispose();
                _loggerScope = new LoggerScope(scope, this);
            }
            return _loggerScope;
        }
        /// <summary>
        /// 开启作用域
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public LoggerScope BeginScope(string scope)
        {
            if (_loggerScope is null)
            {
                _loggerScope = new LoggerScope(scope, this);
            }
            else if (_loggerScope.ScopeName != scope)
            {
                _loggerScope.ScopeName = scope;
            }
            return _loggerScope;
        }
        /// <summary>
        /// 退出域
        /// </summary>
        public void ExitScope() => _loggerScope = null;
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            LoggerConfig loggerConfig = getConfig();
            return logLevel >= loggerConfig.MinLogLevel && logLevel <= loggerConfig.MaxLogLevel;
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            LoggerConfig loggerConfig = getConfig();
            LoggerWriterModel model = new(loggerConfig, categoryName, logLevel, formatter(state, exception), exception, _loggerScope?.CloneScope());
            LoggerHost.WriteLogger(model);
        }
    }
}
