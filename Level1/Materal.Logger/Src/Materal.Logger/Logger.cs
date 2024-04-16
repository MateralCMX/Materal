namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    internal class Logger(string categoryName, LoggerOptions options, ILoggerHost loggerHost) : ILogger
    {
        internal LoggerOptions Options { get; set; } = options;
        private readonly LoggerExternalScopeProvider _scopeProvider = new();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            string message = formatter(state, exception);
            int threadID = Environment.CurrentManagedThreadId;
            LoggerScope loggerScope = new(_scopeProvider);
            loggerHost.Log(new(logLevel, eventId, categoryName, message, exception, threadID, loggerScope));
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => state switch
        {
            LoggerScope => _scopeProvider.Push(state),
            _ => _scopeProvider.Push(new LoggerScope(state)),
        };
        public bool IsEnabled(LogLevel logLevel) => logLevel >= Options.MinLevel && logLevel <= Options.MaxLevel;
    }
}
