using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;

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
        /// 日志域
        /// </summary>
        private LoggerScope? _loggerScope;
        /// <summary>
        /// 日志已关闭
        /// </summary>
        public static bool IsClose { get; private set; } = false;
        /// <summary>
        /// 日志处理数据流块
        /// </summary>
        private static readonly ActionBlock<LoggerHandlerModel> _actionBlock = new(HandlerLog);
        /// <summary>
        /// 服务容器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 处理器集合
        /// </summary>
        private static List<ILoggerHandler>? _handlers;
        /// <summary>
        /// 构造方法
        /// </summary>
        public Logger(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _handlers ??= _serviceProvider.GetServices<ILoggerHandler>().ToList();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public Logger(string categoryName, IServiceProvider serviceProvider) : this(serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return;
            _categoryName = categoryName;
        }
        /// <summary>
        /// 开始范围
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            string? scope = state.ToString();
            if (scope is null || string.IsNullOrWhiteSpace(scope)) return null;
            return BeginScope(scope);
        }
        /// <summary>
        /// 开始域
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public LoggerScope BeginScope(string scope)
        {
            if (_loggerScope is null)
            {
                _loggerScope = new LoggerScope(scope, this);
            }
            else if (_loggerScope.Scope != scope)
            {
                _loggerScope.Scope = scope;
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
        public bool IsEnabled(LogLevel logLevel) => true;
        /// <summary>
        /// 写日志
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            LoggerHandlerModel model = new()
            {
                LogLevel = logLevel,
                Exception = exception,
                Message = formatter(state, exception),
                CategoryName = _categoryName,
                Scope = _loggerScope
            };
            _actionBlock.Post(model);
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        private static void HandlerLog(LoggerHandlerModel model)
        {
            if (_handlers is null) return;
            Parallel.ForEach(_handlers, handler => handler.Handler(model));
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static async Task ShutdownAsync()
        {
            IsClose = true;
            if (_handlers is null) return;
            _actionBlock.Complete();
            await _actionBlock.Completion;
            foreach (ILoggerHandler handler in _handlers)
            {
                await handler.ShutdownAsync();
            }
        }
    }
}
