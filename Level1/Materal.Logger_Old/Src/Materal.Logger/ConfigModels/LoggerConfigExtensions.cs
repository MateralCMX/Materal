namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static class LoggerConfigExtensions
    {
        #region Other
        /// <summary>
        /// 设置日志等级
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="minLogLevel"></param>
        /// <param name="maxLogLevel"></param>
        /// <returns></returns>
        public static LoggerConfig SetLogLevel(this LoggerConfig loggerConfig, LogLevel minLogLevel, LogLevel? maxLogLevel)
        {
            loggerConfig.MinLogLevel = minLogLevel;
            if (maxLogLevel is not null)
            {
                loggerConfig.MaxLogLevel = maxLogLevel.Value;
            }
            return loggerConfig;
        }
        /// <summary>
        /// 设置日志自身等级
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="minLogLevel"></param>
        /// <param name="maxLogLevel"></param>
        /// <returns></returns>
        public static LoggerConfig SetLoggerLogLevel(this LoggerConfig loggerConfig, LogLevel minLogLevel, LogLevel? maxLogLevel)
        {
            loggerConfig.MinLoggerLogLevel = minLogLevel;
            if (maxLogLevel is not null)
            {
                loggerConfig.MaxLoggerLogLevel = maxLogLevel.Value;
            }
            return loggerConfig;
        }
        /// <summary>
        /// 设置应用程序
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        public static LoggerConfig SetApplication(this LoggerConfig loggerConfig, string application)
        {
            loggerConfig.Application = application;
            return loggerConfig;
        }
        /// <summary>
        /// 设置LogLevel
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        public static LoggerConfig SetLogLevels(this LoggerConfig loggerConfig, Dictionary<string, LogLevel> logLevels)
        {
            if (logLevels.Count > 0)
            {
                loggerConfig.LogLevel = logLevels;
            }
            return loggerConfig;
        }
        /// <summary>
        /// 添加LogLevel
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        public static LoggerConfig AddLogLevels(this LoggerConfig loggerConfig, KeyValuePair<string, LogLevel>[] logLevels)
        {
            if (logLevels.Length > 0)
            {
                loggerConfig.LogLevel ??= [];
                foreach (KeyValuePair<string, LogLevel> scope in logLevels)
                {
                    loggerConfig.LogLevel.TryAdd(scope.Key, scope.Value);
                }
            }
            return loggerConfig;
        }
        /// <summary>
        /// 添加LogLevel
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public static LoggerConfig AddLogLevel(this LoggerConfig loggerConfig, string name, LogLevel logLevel)
        {
            loggerConfig.AddLogLevels([new(name, logLevel)]);
            return loggerConfig;
        }
        /// <summary>
        /// 设置Scopes
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static LoggerConfig SetScopes(this LoggerConfig loggerConfig, Dictionary<string, LogLevel> scopes)
        {
            if (scopes.Count > 0)
            {
                loggerConfig.Scopes = scopes;
            }
            return loggerConfig;
        }
        /// <summary>
        /// 添加Scopes
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public static LoggerConfig AddScope(this LoggerConfig loggerConfig, string name, LogLevel logLevel)
        {
            loggerConfig.AddScopes([new(name, logLevel)]);
            return loggerConfig;
        }
        /// <summary>
        /// 添加Scopes
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static LoggerConfig AddScopes(this LoggerConfig loggerConfig, KeyValuePair<string, LogLevel>[] scopes)
        {
            if (scopes.Length > 0)
            {
                loggerConfig.Scopes ??= [];
                foreach (KeyValuePair<string, LogLevel> scope in scopes)
                {
                    loggerConfig.Scopes.TryAdd(scope.Key, scope.Value);
                }
            }
            return loggerConfig;
        }
        #endregion
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
        /// 添加一个目标
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public static LoggerConfig AddTarget(this LoggerConfig loggerConfig, TargetConfig target)
        {
            TargetConfig? oldTarget = LoggerConfig.Targets.FirstOrDefault(m => m.Name == target.Name);
            if(oldTarget is not null)
            {
                LoggerConfig.Targets.Remove(oldTarget);
            }
            if (!LoggerConfig.CodeConfigTargetNames.Contains(target.Name))
            {
                LoggerConfig.CodeConfigTargetNames.Add(target.Name);
            }
            LoggerConfig.Targets.Add(target);
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
        /// <param name="logLevels"></param>
        /// <param name="scopes"></param>
        public static LoggerConfig AddRule(this LoggerConfig loggerConfig, string name, IEnumerable<string> targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? logLevels = null, Dictionary<string, LogLevel>? scopes = null)
        {
            if (!targets.Any()) return loggerConfig;
            minLevel ??= LogLevel.Trace;
            maxLevel ??= LogLevel.Critical;
            RuleConfig rule = new()
            {
                Name = name,
                Targets = targets.ToList(),
                MinLogLevel = minLevel.Value,
                MaxLogLevel = maxLevel.Value
            };
            if (logLevels is not null && logLevels.Count > 0)
            {
                rule.LogLevel = logLevels;
            }
            if (scopes is not null && scopes.Count > 0)
            {
                rule.Scopes = scopes;
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
        /// <param name="logLevels"></param>
        public static LoggerConfig AddRule(this LoggerConfig loggerConfig, string name, string targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? logLevels = null)
            => loggerConfig.AddRule(name, new string[] { targets }, minLevel, maxLevel, logLevels);
        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="ruleConfig"></param>
        /// <returns></returns>
        public static LoggerConfig AddRule(this LoggerConfig loggerConfig, RuleConfig ruleConfig)
        {
            if (loggerConfig.Rules.Any(m => m.Name == ruleConfig.Name)) throw new LoggerException("已存在相同名称的规则");
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
        /// <param name="logLevels"></param>
        public static LoggerConfig AddAllTargetsRule(this LoggerConfig loggerConfig, string name = "全目标规则", LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? logLevels = null)
        {
            List<string> targets = [];
            LoggerConfig.Targets.ForEach(m => targets.Add(m.Name));
            loggerConfig.AddRule(name, targets, minLevel, maxLevel, logLevels);
            return loggerConfig;
        }
        #endregion
    }
}
