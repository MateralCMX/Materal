using Materal.Logger.LoggerHandlers;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger : ILogger
    {
        private static readonly List<LoggerHandler> _loggerHandlers = new();
        /// <summary>
        /// 本地服务
        /// </summary>
        public static LoggerLocalServer? LocalServer { get; private set; }
        private readonly string? _categoryName;
        private static readonly object initLockObject = new();
        private static readonly ActionBlock<LoggerHandlerModel> actionBlock;
        static Logger()
        {
            actionBlock = new(HandlerLog);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public Logger()
        {
            if (_loggerHandlers.Count > 0) return;
            lock (initLockObject)
            {
                if (_loggerHandlers.Count > 0) return;
                foreach (LoggerRuleConfigModel rule in LoggerConfig.RulesConfig)
                {
                    if (!rule.Enable) continue;
                    List<LoggerHandler> tempHandler = LoggerHandlerFactroy.GetLoggerHandlers(rule);
                    _loggerHandlers.AddRange(tempHandler);
                }
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="categoryName"></param>
        public Logger(string categoryName) : this()
        {
            _categoryName = categoryName;
        }
        private LoggerScope? _loggerScope;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public LoggerScope BeginScope(string scope)
        {
            if (_loggerScope == null)
            {
                _loggerScope = new LoggerScope(scope, this);
            }
            else if(_loggerScope.Scope != scope)
            {
                _loggerScope.Scope = scope;
            }
            return _loggerScope;
        }
        /// <summary>
        /// 开启域
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            string? scope = state.ToString();
            if (scope == null) return null;
            return BeginScope(scope);
        }
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
        public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            LoggerHandlerModel model = new()
            {
                LogLevel = logLevel,
                Exception = exception,
                Message = formatter(state, exception),
                CategoryName = _categoryName,
                MateralLoggerScope = _loggerScope
            };
            actionBlock.Post(model);
        }
        /// <summary>
        /// 退出域
        /// </summary>
        public void ExitScope()
        {
            _loggerScope = null;
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        public static void InitServer()
        {
            if (!LoggerConfig.ServerConfig.Enable) return;
            LocalServer = new LoggerLocalServer();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static void Shutdown()
        {
            actionBlock.Complete();
            actionBlock.Completion.Wait();
            foreach (LoggerHandler item in _loggerHandlers)
            {
                item.Close();
            }
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        private static void HandlerLog(LoggerHandlerModel model)
        {
            foreach (LoggerHandler loggerHandler in _loggerHandlers)
            {
                if (!loggerHandler.CanRun(model.LogLevel, model.CategoryName)) continue;
                loggerHandler.Handler(model.LogLevel, model.Message, model.CategoryName, model.MateralLoggerScope, DateTime.Now, model.Exception, model.ThreadID);
            }
        }
    }
}
