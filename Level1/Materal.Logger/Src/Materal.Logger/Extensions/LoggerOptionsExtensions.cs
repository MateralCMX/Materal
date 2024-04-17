namespace Materal.Logger.Extensions
{
    /// <summary>
    /// LoggerOptions扩展
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        #region Other
        /// <summary>
        /// 设置日志等级
        /// </summary>
        /// <param name="options"></param>
        /// <param name="minLogLevel"></param>
        /// <param name="maxLogLevel"></param>
        /// <returns></returns>
        public static LoggerOptions SetLogLevel(this LoggerOptions options, LogLevel minLogLevel, LogLevel? maxLogLevel)
        {
            options.MinLevel = minLogLevel;
            if (maxLogLevel is not null)
            {
                options.MaxLevel = maxLogLevel.Value;
            }
            return options;
        }
        /// <summary>
        /// 设置日志主机日志等级
        /// </summary>
        /// <param name="options"></param>
        /// <param name="minLogLevel"></param>
        /// <param name="maxLogLevel"></param>
        /// <returns></returns>
        public static LoggerOptions SetLoggerHostLogLevel(this LoggerOptions options, LogLevel minLogLevel, LogLevel? maxLogLevel = null)
        {
            options.MinLoggerInfoLevel = minLogLevel;
            if (maxLogLevel is not null)
            {
                options.MaxLoggerInfoLevel = maxLogLevel.Value;
            }
            return options;
        }
        /// <summary>
        /// 设置应用程序
        /// </summary>
        /// <param name="options"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        public static LoggerOptions SetApplication(this LoggerOptions options, string application)
        {
            options.Application = application;
            return options;
        }
        /// <summary>
        /// 设置LogLevel
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        public static LoggerOptions SetLogLevels(this LoggerOptions options, Dictionary<string, LogLevel> logLevels)
        {
            if (logLevels.Count > 0)
            {
                options.LogLevel = logLevels;
            }
            return options;
        }
        /// <summary>
        /// 添加LogLevel
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        public static LoggerOptions AddLogLevels(this LoggerOptions options, KeyValuePair<string, LogLevel>[] logLevels)
        {
            if (logLevels.Length > 0)
            {
                options.LogLevel ??= [];
                foreach (KeyValuePair<string, LogLevel> scope in logLevels)
                {
                    options.LogLevel.TryAdd(scope.Key, scope.Value);
                }
            }
            return options;
        }
        /// <summary>
        /// 添加LogLevel
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public static LoggerOptions AddLogLevel(this LoggerOptions options, string name, LogLevel logLevel)
        {
            options.AddLogLevels([new(name, logLevel)]);
            return options;
        }
        /// <summary>
        /// 设置Scopes
        /// </summary>
        /// <param name="options"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static LoggerOptions SetScopes(this LoggerOptions options, Dictionary<string, LogLevel> scopes)
        {
            if (scopes.Count > 0)
            {
                options.Scopes = scopes;
            }
            return options;
        }
        /// <summary>
        /// 添加Scopes
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public static LoggerOptions AddScope(this LoggerOptions options, string name, LogLevel logLevel)
        {
            options.AddScopes([new(name, logLevel)]);
            return options;
        }
        /// <summary>
        /// 添加Scopes
        /// </summary>
        /// <param name="options"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static LoggerOptions AddScopes(this LoggerOptions options, KeyValuePair<string, LogLevel>[] scopes)
        {
            if (scopes.Length > 0)
            {
                options.Scopes ??= [];
                foreach (KeyValuePair<string, LogLevel> scope in scopes)
                {
                    options.Scopes.TryAdd(scope.Key, scope.Value);
                }
            }
            return options;
        }
        #endregion
        #region CustomData
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static LoggerOptions TryAddCustomData(this LoggerOptions options, string key, object? value)
        {
            options.CustomData.TryAdd(key, value);
            return options;
        }
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static LoggerOptions AddCustomData(this LoggerOptions options, string key, object? value)
        {
            if (options.CustomData.TryAdd(key, value)) return options;
            options.CustomData[key] = value;
            return options;
        }
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="key"></param>
        public static LoggerOptions RemoveCustomData(this LoggerOptions options, string key)
        {
            if (!options.CustomData.ContainsKey(key)) return options;
            options.CustomData.Remove(key);
            return options;
        }
        #endregion
        #region Target
        /// <summary>
        /// 添加一个目标
        /// </summary>
        /// <param name="options"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public static LoggerOptions AddTarget(this LoggerOptions options, LoggerTargetOptions target)
        {
            LoggerTargetOptions? oldTarget = options.Targets.FirstOrDefault(m => m.Name == target.Name);
            if (oldTarget is not null)
            {
                options.Targets.Remove(oldTarget);
            }
            if (!options.CodeConfigTargetNames.Contains(target.Name))
            {
                options.CodeConfigTargetNames.Add(target.Name);
            }
            options.Targets.Add(target);
            options.UpdateTargetOptions();
            return options;
        }
        #endregion
        #region Rule
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="logLevels"></param>
        /// <param name="scopes"></param>
        public static LoggerOptions AddRule(this LoggerOptions options, string name, IEnumerable<string> targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? logLevels = null, Dictionary<string, LogLevel>? scopes = null)
        {
            if (!targets.Any()) return options;
            minLevel ??= LogLevel.Trace;
            maxLevel ??= LogLevel.Critical;
            LoggerRuleOptions rule = new()
            {
                Name = name,
                Targets = targets.ToList(),
                MinLevel = minLevel.Value,
                MaxLevel = maxLevel.Value
            };
            if (logLevels is not null && logLevels.Count > 0)
            {
                rule.LogLevel = logLevels;
            }
            if (scopes is not null && scopes.Count > 0)
            {
                rule.Scopes = scopes;
            }
            options.AddRule(rule);
            return options;
        }
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="logLevels"></param>
        public static LoggerOptions AddRule(this LoggerOptions options, string name, string targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? logLevels = null)
            => options.AddRule(name, new string[] { targets }, minLevel, maxLevel, logLevels);
        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="options"></param>
        /// <param name="ruleConfig"></param>
        /// <returns></returns>
        public static LoggerOptions AddRule(this LoggerOptions options, LoggerRuleOptions ruleConfig)
        {
            if (options.Rules.Any(m => m.Name == ruleConfig.Name)) throw new LoggerException("已存在相同名称的规则");
            options.Rules.Add(ruleConfig);
            options.UpdateTargetOptions();
            return options;
        }
        /// <summary>
        /// 添加一个全目标规则
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="logLevels"></param>
        public static LoggerOptions AddAllTargetsRule(this LoggerOptions options, string name = "全目标规则", LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? logLevels = null)
        {
            List<string> targets = [];
            options.Targets.ForEach(m => targets.Add(m.Name));
            options.AddRule(name, targets, minLevel, maxLevel, logLevels);
            return options;
        }
        #endregion
    }
}
