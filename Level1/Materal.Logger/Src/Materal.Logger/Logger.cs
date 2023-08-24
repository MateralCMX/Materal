using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        private readonly string? _categoryName;
        /// <summary>
        /// 构造方法
        /// </summary>
        public Logger()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="categoryName"></param>
        public Logger(string categoryName) : this()
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return;
            _categoryName = categoryName;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
