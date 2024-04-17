namespace Materal.Logger
{
    /// <summary>
    /// 日志写入器基类
    /// </summary>
    /// <typeparam name="TLoggerTargetOptions"></typeparam>
    public abstract class BaseLoggerWriter<TLoggerTargetOptions>(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : ILoggerWriter, ILoggerWriter<TLoggerTargetOptions>
        where TLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 日志配置
        /// </summary>
        protected IOptionsMonitor<LoggerOptions> Options { get; } = options;
        /// <summary>
        /// 日志记录器信息
        /// </summary>
        protected ILoggerInfo LoggerInfo { get; } = loggerInfo;
        /// <summary>
        /// 是否关闭
        /// </summary>
        protected bool IsClose { get; set; } = false;
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        public virtual async Task LogAsync(Log log, LoggerRuleOptions ruleOptions, LoggerTargetOptions targetOptions)
        {
            if (!await CanLogAsync(log, ruleOptions, targetOptions)) return;
            if (targetOptions is not TLoggerTargetOptions loggerTargetOptions) return;
            await LogAsync(log, ruleOptions, loggerTargetOptions);
        }
        /// <summary>
        /// 是否可以写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <returns></returns>
        protected virtual async Task<bool> CanLogAsync(Log log, LoggerRuleOptions ruleOptions, LoggerTargetOptions targetOptions)
        {
            if (IsClose) return false;
            if (log.Level == LogLevel.None) return false;
            if (!ruleOptions.Enable) return false;
            if (!targetOptions.Enable) return false;
            if (log.Level < Options.CurrentValue.MinLevel || log.Level > Options.CurrentValue.MaxLevel) return false;
            if (log.Level < ruleOptions.MinLevel || log.Level > ruleOptions.MaxLevel) return false;
            if (!ruleOptions.Targets.Contains(targetOptions.Name)) return false;
            if (targetOptions is not TLoggerTargetOptions loggerTargetOptions) return false;
            return await CanLogAsync(log, ruleOptions, loggerTargetOptions);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        public abstract Task LogAsync(Log log, LoggerRuleOptions ruleOptions, TLoggerTargetOptions targetOptions);
        /// <summary>
        /// 是否可以写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <returns></returns>
        protected virtual async Task<bool> CanLogAsync(Log log, LoggerRuleOptions ruleOptions, TLoggerTargetOptions targetOptions)
        {
            #region 验证作用域
            if (ruleOptions.Scopes is not null && ruleOptions.Scopes.Count > 0)
            {
                if (!CanWriteLoggerByScopes(log, ruleOptions.Scopes)) return false;
            }
            else if (Options.CurrentValue.Scopes is not null && Options.CurrentValue.Scopes.Count > 0)
            {
                if (!CanWriteLoggerByScopes(log, Options.CurrentValue.Scopes)) return false;
            }
            #endregion
            #region 验证命名空间
            if (ruleOptions.LogLevel is not null && ruleOptions.LogLevel.Count > 0)
            {
                if (!CanWriteLoggerByLogLevels(log, ruleOptions.LogLevel)) return false;
            }
            else if (Options.CurrentValue.LogLevel is not null && Options.CurrentValue.LogLevel.Count > 0)
            {
                if (!CanWriteLoggerByLogLevels(log, Options.CurrentValue.LogLevel)) return false;
            }
            else if (LoggerOptions.DefaultLogLevel is not null && LoggerOptions.DefaultLogLevel.Count > 0)
            {
                if (!CanWriteLoggerByLogLevels(log, LoggerOptions.DefaultLogLevel)) return false;
            }
            #endregion
            return await Task.FromResult(true);
        }
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLoggerByScopes(Log log, Dictionary<string, LogLevel>? scopes)
            => CanWriteLogger(log, log.ScopeName, scopes);
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLoggerByLogLevels(Log log, Dictionary<string, LogLevel>? logLevels)
            => CanWriteLogger(log, log.CategoryName, logLevels);
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="value"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLogger(Log log, string? value, Dictionary<string, LogLevel>? dic)
        {
            if (dic is null) return true;
            LogLevel? configLogLevel = null;
            if (value is not null && !string.IsNullOrWhiteSpace(value))
            {
                int index = 0;
                foreach (string key in dic.Keys)
                {
                    if (value == key)
                    {
                        configLogLevel = dic[key];
                        break;
                    }
                    if (!value.StartsWith(key)) continue;
                    int nowIndex = key.Split('.').Length;
                    if (index > nowIndex) continue;
                    index = nowIndex;
                    configLogLevel = dic[key];
                }
            }
            if (configLogLevel is null && dic.TryGetValue("Default", out LogLevel logLevelValue))
            {
                configLogLevel = logLevelValue;
            }
            if (configLogLevel is null) return true;
            if (configLogLevel == LogLevel.None) return false;
            bool result = configLogLevel <= log.Level;
            return result;
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public virtual async Task StartAsync()
        {
            IsClose = false;
            await Task.CompletedTask;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public virtual async Task StopAsync()
        {
            IsClose = true;
            await Task.CompletedTask;
        }
    }
}
