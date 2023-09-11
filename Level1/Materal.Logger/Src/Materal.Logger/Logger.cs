﻿using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Utils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;

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
        /// 处理器集合
        /// </summary>
        public static List<ILoggerHandler> Handlers { get; } = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public Logger(string categoryName)
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
        public IDisposable BeginScope<TState>(TState state)
        {
            string scope;
            if (state is not null)
            {
                if(state is AdvancedScope advancedScope)
                {
                    return BeginScope(advancedScope);
                }
                if (state is string stateString)
                {
                    scope = stateString;
                }
                else
                {
                    scope = state.ToString();
                }
            }
            else
            {
                scope = "PublicScope";
            }
            return BeginScope(scope);
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
                    if(advancedScope is not null)
                    {
                        model.Scope = new LoggerScope(advancedScope);
                    }
                }
                model.Scope ??= new LoggerScope(_loggerScope.ScopeName);
            }
            _actionBlock.Post(model);
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        private static void HandlerLog(LoggerHandlerModel model)
        {
            if (Handlers is null) return;
            List<LoggerRuleConfigModel> rules = LoggerConfig.Rules;
            List<LoggerTargetConfigModel> targets = LoggerConfig.Targets;
            Dictionary<string, LogLevel> defaultLogLevels = LoggerConfig.DefaultLogLevels;
            Parallel.ForEach(Handlers, handler => handler.Handler(model, rules, targets, defaultLogLevels));
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static async Task ShutdownAsync()
        {
            IsClose = true;
            if (LoggerConfig.Mode == LoggerModeEnum.Normal) return;
            if (Handlers is null) return;
            _actionBlock.Complete();
            await _actionBlock.Completion;
            foreach (ILoggerHandler handler in Handlers)
            {
                await handler.ShutdownAsync();
            }
        }
    }
}
