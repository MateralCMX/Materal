using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    internal class Logger(string categoryName, ILoggerListener loggerListener) : ILogger
    {
        private readonly LoggerExternalScopeProvider _scopeProvider = new();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            string message = formatter(state, exception);
            Log log = new(logLevel, eventId, categoryName, message, exception);
            loggerListener.Log(log);
        }
        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => throw new NotImplementedException();
    }
}
