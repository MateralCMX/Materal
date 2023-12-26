using Materal.Logger.ConsoleLogger;
using Materal.Logger.FileLogger;
using System.Diagnostics;

namespace Materal.Logger.Extensions
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加TraceListener
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="traceListener"></param>
        /// <returns></returns>
        public static LoggerConfig AddTraceListener(this LoggerConfig loggerConfig, TraceListener traceListener)
        {
            LoggerHost.AddTraceListener(traceListener);
            Trace.Listeners.Add(traceListener);
            return loggerConfig;
        }
        #region CustomConfig
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static LoggerConfig TryAddCustomConfig(this LoggerConfig loggerConfig, string key, object? value)
        {
            LoggerConfig.CustomConfig.TryAdd(key, value);
            return loggerConfig;
        }
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static LoggerConfig AddCustomConfig(this LoggerConfig loggerConfig, string key, object? value)
        {
            if (LoggerConfig.CustomConfig.TryAdd(key, value)) return loggerConfig;
            LoggerConfig.CustomConfig[key] = value;
            return loggerConfig;
        }
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="key"></param>
        public static LoggerConfig RemoveCustomConfig(this LoggerConfig loggerConfig, string key)
        {
            if (!LoggerConfig.CustomConfig.ContainsKey(key)) return loggerConfig;
            LoggerConfig.CustomConfig.Remove(key);
            return loggerConfig;
        }
        #endregion
        #region Target
        /// <summary>
        /// 添加一个控制台目标
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public static LoggerConfig AddConsoleTarget(this LoggerConfig loggerConfig, string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            ConsoleLoggerTargetConfig target = new()
            {
                Name = name
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            if (colors is not null)
            {
                target.Colors = new ColorsConfig(colors);
            }
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
        /// <summary>
        /// 添加一个文件输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        public static LoggerConfig AddFileTarget(this LoggerConfig loggerConfig, string name, string path, string? format = null)
        {
            FileLoggerTargetConfig target = new()
            {
                Name = name,
                Path = path
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
        ///// <summary>
        ///// 添加一个Http输出
        ///// </summary>
        ///// <param name="loggerConfig"></param>
        ///// <param name="name"></param>
        ///// <param name="url"></param>
        ///// <param name="httpMethod"></param>
        //public static LoggerConfig AddHttpTarget(this LoggerConfig loggerConfig, string name, string url, HttpMethod? httpMethod = null)
        //{
        //    HttpLoggerTargetConfig target = new()
        //    {
        //        Name = name,
        //        Url = url
        //    };
        //    if (httpMethod is not null)
        //    {
        //        target.HttpMethod = httpMethod.Method;
        //    }
        //    loggerConfig.AddTarget(target);
        //    return loggerConfig;
        //}
        /// <summary>
        /// 添加一个目标
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public static LoggerConfig AddTarget(this LoggerConfig loggerConfig, TargetConfig target)
        {
            if (loggerConfig.Targets.Any(m => m.Name == target.Name)) throw new LoggerException("已存在相同名称的目标");
            loggerConfig.Targets.Add(target);
            return loggerConfig;
        }
        #endregion
        #region Rule
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        /// <param name="scopes"></param>
        public static LoggerConfig AddRule(this LoggerConfig loggerConfig, string name, IEnumerable<string> targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null, IEnumerable<string>? scopes = null)
        {
            if (!targets.Any()) return loggerConfig;
            minLevel ??= LogLevel.Information;
            maxLevel ??= LogLevel.Critical;
            if (loglevels is null || loglevels.Count <= 0)
            {
                loglevels = new Dictionary<string, LogLevel>() { ["Default"] = minLevel.Value };
            }
            RuleConfig rule = new()
            {
                Name = name,
                Targets = targets.ToList(),
                MinLogLevel = minLevel.Value,
                MaxLogLevel = maxLevel.Value,
                LogLevel = loglevels
            };
            if (scopes is not null)
            {
                rule.Scopes = scopes.ToList();
            }
            loggerConfig.AddRule(rule);
            return loggerConfig;
        }
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        public static LoggerConfig AddRule(this LoggerConfig loggerConfig, string name, string targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
            => loggerConfig.AddRule(name, new string[] { targets }, minLevel, maxLevel, loglevels);
        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="ruleConfig"></param>
        /// <returns></returns>
        public static LoggerConfig AddRule(this LoggerConfig loggerConfig, RuleConfig ruleConfig)
        {
            loggerConfig.Rules.Add(ruleConfig);
            return loggerConfig;
        }
        /// <summary>
        /// 添加一个全目标规则
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        public static LoggerConfig AddAllTargetsRule(this LoggerConfig loggerConfig, string name = "全目标规则", LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
        {
            List<string> targets = [];
            loggerConfig.Targets.ForEach(m => targets.Add(m.Name));
            loggerConfig.AddRule(name, targets, minLevel, maxLevel, loglevels);
            return loggerConfig;
        }
        #endregion
    }
}
