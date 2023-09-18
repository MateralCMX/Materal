using Materal.Logger.LoggerHandlers.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    [Serializable]
    public class Logger : ILogger
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        private readonly string? _categoryName;
        private readonly LoggerRuntime _loggerHelper;
        /// <summary>
        /// 日志域
        /// </summary>
        private LoggerScope? _loggerScope;
        /// <summary>
        /// 构造方法
        /// </summary>
        public Logger(string categoryName, IServiceProvider serviceProvider)
        {
            _loggerHelper = serviceProvider.GetRequiredService<LoggerRuntime>();
            if (string.IsNullOrWhiteSpace(categoryName)) return;
            _categoryName = categoryName;
        }
        /// <summary>
        /// 开始范围
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            _loggerScope = new LoggerScope("TestScope");
            return _loggerScope;
            //string scope;
            //LoggerScope? loggerScope = null;
            //if (state is not null)
            //{
            //    if (state is AdvancedScope advancedScope)
            //    {
            //        loggerScope = BeginScope(advancedScope);
            //    }
            //    if (state is string stateString)
            //    {
            //        scope = stateString;
            //    }
            //    else
            //    {
            //        scope = state.ToString();
            //    }
            //}
            //else
            //{
            //    scope = "PublicScope";
            //}
            //loggerScope ??= BeginScope(scope);
            //LoggerLog.LogDebug($"已开启日志域{loggerScope.ScopeName}");
            //return loggerScope;
        }
        /// <summary>
        /// 开始域
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
                CategoryName = _categoryName
            };
            if (_loggerScope is not null)
            {
                if (_loggerScope.AdvancedScope is not null)
                {
                    AdvancedScope? advancedScope = _loggerScope.AdvancedScope.Clone();
                    if (advancedScope is not null)
                    {
                        model.Scope = new LoggerScope(advancedScope);
                    }
                }
                model.Scope ??= new LoggerScope(_loggerScope.ScopeName);
            }
            _loggerHelper.Handler(model);
        }
    }
}
