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
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <param name="loggerLog"></param>
        public abstract void Handler(LoggerHandlerModel model, LoggerConfig config, LoggerLog loggerLog);
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="loggerLog"></param>
        /// <returns></returns>
        public virtual Task ShutdownAsync(LoggerLog loggerLog) => Task.CompletedTask;
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
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <param name="loggerLog"></param>
        /// <returns></returns>
        public override void Handler(LoggerHandlerModel model, LoggerConfig config, LoggerLog loggerLog)
        {
            loggerLog.LogDebug("正在处理日志");
            List<T> trueTargets = config.GetAllTargets<T>();
            Handler(model, config, trueTargets, loggerLog);
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <param name="targets"></param>
        /// <param name="loggerLog"></param>
        /// <returns></returns>
        public void Handler(LoggerHandlerModel model, LoggerConfig config, List<T> targets, LoggerLog loggerLog)
        {
            Dictionary<string, LogLevel> defaultLogLevels = config.DefaultLogLevels;
            foreach (LoggerRuleConfigModel rule in config.Rules)
            {
                if (!rule.Enable || !CanRun(rule, model.LogLevel, model.CategoryName, defaultLogLevels)) continue;
                foreach (string targetName in rule.Targets)
                {
                    T? target = targets.FirstOrDefault(m => m.Name == targetName && m.Enable);
                    if (target is null) continue;
                    Handler(rule, target, model, config, loggerLog);
                }
            }
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <param name="loggerLog"></param>
        protected abstract void Handler(LoggerRuleConfigModel rule, T target, LoggerHandlerModel model, LoggerConfig config, LoggerLog loggerLog);
    }
}
