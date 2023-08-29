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
        /// 默认日志等级组
        /// </summary>
        protected static Dictionary<string, LogLevel> DefaultLogLevels => LoggerConfig.DefaultLogLevels;
        /// <summary>
        /// 所有规则
        /// </summary>
        protected static List<LoggerRuleConfigModel> AllRules => LoggerConfig.Rules;
        /// <summary>
        /// 所有目标
        /// </summary>
        protected static List<LoggerTargetConfigModel> AllTargets => LoggerConfig.Targets;
        /// <summary>
        /// 目标类型
        /// </summary>
        protected virtual string TargetType
        {
            get
            {
                string name = GetType().Name;
                if (!name.EndsWith(nameof(LoggerHandler))) return string.Empty;
                return name[..^nameof(LoggerHandler).Length];
            }
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual void Handler(LoggerHandlerModel model)
        {
            List<LoggerTargetConfigModel> allTargets = AllTargets;
            foreach (LoggerRuleConfigModel rule in AllRules)
            {
                if (!rule.Enable || !CanRun(rule, model.LogLevel, model.CategoryName)) continue;
                foreach (string targetName in rule.Targets)
                {
                    LoggerTargetConfigModel? target = allTargets.FirstOrDefault(m => m.Name == targetName && m.Type == TargetType && m.Enable);
                    if (target is null) continue;
                    try
                    {
                        Handler(rule, target, model);
                    }
                    catch (Exception ex)
                    {
                        LoggerLog.LogError("日志记录出错", ex);
                    }
                }
            }
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected abstract void Handler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model);
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
        /// <returns></returns>
        protected virtual bool CanRun(LoggerRuleConfigModel rule, LogLevel logLevel, string? categoryName)
        {
            if (logLevel == LogLevel.None) return false;
            if (rule.MinLevel > logLevel || rule.MaxLevel < logLevel) return false;
            LogLevel? configLogLevel = null;
            Dictionary<string, LogLevel> logLevels = rule.LogLevels ?? DefaultLogLevels;
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
}
