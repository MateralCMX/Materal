using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 控制台日志记录器
    /// </summary>
    /// <param name="name"></param>
    /// <param name="config"></param>
    public sealed class ConsoleLogger(string name, MateralLoggerConfig config) : ILogger
    {
        /// <summary>
        /// 开启作用域
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => logLevel >= config.MinLogLevel && logLevel <= config.MaxLogLevel;
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
            Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");
            Console.Write($"     {name} - ");
            Console.Write($"{formatter(state, exception)}");
            Console.WriteLine();
        }
    }
}
