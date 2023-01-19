using Materal.Logger.LoggerHandlers;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.Logger
{
    public class MateralLogger : ILogger
    {
        private static readonly List<LoggerHandler> _loggerHandlers = new();
        public static MateralLoggerLocalServer? LocalServer { get; private set; }
        private readonly string? _categoryName;
        private static readonly object initLockObject = new();
        private static readonly ActionBlock<LoggerHandlerModel> actionBlock;
        static MateralLogger()
        {
            actionBlock = new(HandlerLog);
        }
        public MateralLogger()
        {
            if (_loggerHandlers.Count > 0) return;
            lock (initLockObject)
            {
                if (_loggerHandlers.Count > 0) return;
                foreach (MateralLoggerRuleConfigModel rule in MateralLoggerConfig.RulesConfig)
                {
                    if (!rule.Enable) continue;
                    List<LoggerHandler> tempHandler = LoggerHandlerFactroy.GetLoggerHandlers(rule);
                    _loggerHandlers.AddRange(tempHandler);
                }
            }
        }
        public MateralLogger(string categoryName) : this()
        {
            _categoryName = categoryName;
        }
        private MateralLoggerScope? _loggerScope;
        public MateralLoggerScope BeginScope(string scope)
        {
            if (_loggerScope == null)
            {
                _loggerScope = new MateralLoggerScope(scope, this);
            }
            else if(_loggerScope.Scope != scope)
            {
                _loggerScope.Scope = scope;
            }
            return _loggerScope;
        }
        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            string? scope = state.ToString();
            if (scope == null) return null;
            return BeginScope(scope);
        }
        public bool IsEnabled(LogLevel logLevel) => true;
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
        public void ExitScope()
        {
            _loggerScope = null;
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        public static void InitServer()
        {
            if (!MateralLoggerConfig.ServerConfig.Enable) return;
            LocalServer = new MateralLoggerLocalServer();
        }
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
