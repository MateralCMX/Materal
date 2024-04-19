using Materal.Utils;

namespace Materal.MergeBlock
{
    internal class MergeBlockLogger : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = formatter(state, exception);
            message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|MergeBlock|{logLevel}|{message}";
            ConsoleQueue.WriteLine(message);
        }
    }
}
