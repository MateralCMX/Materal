using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 日志处理器
    /// </summary>
    public abstract class LoggerHandler : ILoggerHandler
    {
        /// <summary>
        /// 日志运行时
        /// </summary>
        protected readonly LoggerRuntime _loggerRuntime;
        /// <summary>
        /// 日志配置对象
        /// </summary>
        protected LoggerConfig Config => _loggerRuntime.Config;
        /// <summary>
        /// 日志自身日志对象
        /// </summary>
        protected ILoggerLog LoggerLog => _loggerRuntime.LoggerLog;
        /// <summary>
        /// 是否关闭
        /// </summary>
        protected bool IsClose => _loggerRuntime.IsClose;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected LoggerHandler(LoggerRuntime loggerRuntime)
        {
            _loggerRuntime = loggerRuntime;
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        public abstract void Handler(LoggerHandlerModel model);
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public virtual Task ShutdownAsync() => Task.CompletedTask;
        /// <summary>
        /// 是否运行
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="logLevel"></param>
        /// <param name="categoryName"></param>
        /// <param name="defaultLogLevels"></param>
        /// <returns></returns>
        protected virtual bool CanRun(LoggerRuleConfigModel rule, LogLevel logLevel, string? categoryName, Dictionary<string, LogLevel> defaultLogLevels)
        {
            if (logLevel == LogLevel.None) return false;
            if (rule.MinLevel > logLevel || rule.MaxLevel < logLevel) return false;
            LogLevel? configLogLevel = null;
            Dictionary<string, LogLevel> logLevels = rule.LogLevels ?? defaultLogLevels;
            if (categoryName is not null && !string.IsNullOrWhiteSpace(categoryName))
            {
                int index = 0;
                foreach (string key in logLevels.Keys)
                {
                    if (categoryName == key)
                    {
                        configLogLevel = logLevels[key];
                        break;
                    }
                    if (!categoryName.StartsWith(key)) continue;
                    int nowIndex = key.Split('.').Length;
                    if (index > nowIndex) continue;
                    index = nowIndex;
                    configLogLevel = logLevels[key];
                }
            }
            if (configLogLevel is null && logLevels.ContainsKey("Default"))
            {
                configLogLevel = logLevels["Default"];
            }
            if (configLogLevel is null) return true;
            if (configLogLevel == LogLevel.None) return false;
            bool result = configLogLevel <= logLevel;
            return result;
        }
    }
    /// <summary>
    /// 日志处理器
    /// </summary>
    public abstract class LoggerHandler<T> : LoggerHandler, ILoggerHandler
        where T : LoggerTargetConfigModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected LoggerHandler(LoggerRuntime loggerRuntime) : base(loggerRuntime)
        {
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override void Handler(LoggerHandlerModel model)
        {
            List<T> trueTargets = Config.GetAllTargets<T>();
            Handler(model, trueTargets);
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public virtual void Handler(LoggerHandlerModel model, List<T> targets)
        {
            Dictionary<string, LogLevel> defaultLogLevels = Config.DefaultLogLevels;
            foreach (LoggerRuleConfigModel rule in Config.Rules)
            {
                if (!rule.Enable || !CanRun(rule, model.LogLevel, model.CategoryName, defaultLogLevels)) continue;
                foreach (string targetName in rule.Targets)
                {
                    T? target = targets.FirstOrDefault(m => m.Name == targetName && m.Enable);
                    if (target is null) continue;
                    LoggerLog.LogDebug($"正在处理日志:{rule.Name}->{targetName}->{model.LogLevel}->{model.ID}");
                    Handler(rule, target, model);
                }
            }
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected abstract void Handler(LoggerRuleConfigModel rule, T target, LoggerHandlerModel model);
    }
}
