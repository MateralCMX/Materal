using Materal.Logger.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

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
        protected virtual string TargetType {
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
        public void Handler(LoggerHandlerModel model)
        {
            List<LoggerTargetConfigModel> allTargets = AllTargets;
            foreach (LoggerRuleConfigModel rule in AllRules)
            {
                if (!CanRun(rule, model.LogLevel, model.CategoryName)) continue;
                foreach (string targetName in rule.Targets)
                {
                    LoggerTargetConfigModel? target = allTargets.FirstOrDefault(m => m.Name == targetName && m.Type == TargetType);
                    if (target is null) continue;
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
            if (rule.MinLevel > logLevel || rule.MaxLevel < logLevel) return false;
            Dictionary<string, LogLevel> logLevels = rule.LogLevels ?? DefaultLogLevels;
            bool? result = null;
            if (categoryName is not null && !string.IsNullOrWhiteSpace(categoryName))
            {
                int index = 0;
                foreach (string key in logLevels.Keys)
                {
                    if (categoryName == key)
                    {
                        result = logLevels[key] <= logLevel;
                        break;
                    }
                    if (!categoryName.StartsWith(key)) continue;
                    int nowIndex = key.Split('.').Length;
                    if (index > nowIndex) continue;
                    index = nowIndex;
                    result = logLevels[key] <= logLevel;
                }
            }
            if (result is null && logLevels.ContainsKey("Default"))
            {
                result = logLevels["Default"] <= logLevel;
            }
            result ??= true;
            return result.Value;
        }
    }
    /// <summary>
    /// 日志处理器模型
    /// </summary>
    public class LoggerHandlerModel
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; set; }
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; set; } = Environment.CurrentManagedThreadId.ToString();
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; set; }
        /// <summary>
        /// 域
        /// </summary>
        public LoggerScope? Scope { get; set; }
    }
}
