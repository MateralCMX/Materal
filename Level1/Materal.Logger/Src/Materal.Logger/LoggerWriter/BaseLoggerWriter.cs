namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 基础日志写入器
    /// </summary>
    public abstract class BaseLoggerWriter<TTarget>(TTarget target) : ILoggerWriter
        where TTarget : TargetConfig
    {
        /// <summary>
        /// 目标配置
        /// </summary>
        protected TTarget Target => target;
        /// <summary>
        /// 是否关闭
        /// </summary>
        protected bool IsClose { get; set; } = false;
        /// <summary>
        /// 日志配置变更事件
        /// </summary>
        public virtual Action<LoggerConfig>? OnLoggerConfigChanged => null;
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public virtual async Task ShutdownAsync()
        {
            IsClose = true;
            LoggerHost.LoggerLog?.LogDebug($"正在关闭[{Target.Name}]");
            await Task.CompletedTask;
            LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]关闭成功");
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task WriteLoggerAsync(LoggerWriterModel model)
        {
            if (!CanWriteLogger(model)) return;
            await WriteAsync(model);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract Task WriteAsync(LoggerWriterModel model);
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLogger(LoggerWriterModel model)
        {
            if (!model.Config.Enable) return false;
            if (!target.Enable) return false;
            if (model.LogLevel < model.Config.MinLogLevel || model.LogLevel > model.Config.MaxLogLevel) return false;
            foreach (RuleConfig rule in model.Config.Rules)
            {
                if (CanWriteLogger(model, rule)) return true;
            }
            return false;
        }
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLogger(LoggerWriterModel model, RuleConfig rule)
        {
            if (model.LogLevel == LogLevel.None || !rule.Enable) return false;
            if (model.LogLevel < rule.MinLogLevel || model.LogLevel > rule.MaxLogLevel) return false;
            if (!rule.Targets.Contains(target.Name)) return false;
            #region 验证作用域
            if (rule.Scopes is not null && rule.Scopes.Count > 0)
            {
                if (!CanWriteLoggerByScopes(model, rule.Scopes)) return false;
            }
            else if (model.Config.Scopes is not null && model.Config.Scopes.Count > 0)
            {
                if (!CanWriteLoggerByScopes(model, model.Config.Scopes)) return false;
            }
            #endregion
            #region 验证命名空间
            if (rule.LogLevel is not null && rule.LogLevel.Count > 0)
            {
                if (!CanWriteLoggerByLogLevels(model, rule.LogLevel)) return false;
            }
            else if (model.Config.LogLevel is not null && model.Config.LogLevel.Count > 0)
            {
                if (!CanWriteLoggerByLogLevels(model, model.Config.LogLevel)) return false;
            }
            else if (LoggerConfig.DefaultLogLevel is not null && LoggerConfig.DefaultLogLevel.Count > 0)
            {
                if (!CanWriteLoggerByLogLevels(model, LoggerConfig.DefaultLogLevel)) return false;
            }
            #endregion
            return true;
        }
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLoggerByScopes(LoggerWriterModel model, Dictionary<string, LogLevel>? scopes)
            => CanWriteLogger(model, model.Scope?.ScopeName, scopes);
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLoggerByLogLevels(LoggerWriterModel model, Dictionary<string, LogLevel>? logLevels) 
            => CanWriteLogger(model, model.CategoryName, logLevels);
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="value"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLogger(LoggerWriterModel model, string? value, Dictionary<string, LogLevel>? dic)
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
            bool result = configLogLevel <= model.LogLevel;
            return result;
        }
    }
    /// <summary>
    /// 基础日志写入器
    /// </summary>
    public abstract class BaseLoggerWriter<TModel, TTarget>(TTarget target) : BaseLoggerWriter<TTarget>(target), ILoggerWriter<TModel>, ILoggerWriter
        where TModel : LoggerWriterModel
        where TTarget : TargetConfig
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task WriteAsync(LoggerWriterModel model)
        {
            Type modelType = typeof(TModel);
            ConstructorInfo[] constructorInfos = modelType.GetConstructors();
            ConstructorInfo? targetConstructorInfo = null;
            object?[]? parameters = null;
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length == 2 && parameterInfos[0].ParameterType == typeof(LoggerWriterModel) && parameterInfos[1].ParameterType == typeof(TTarget))
                {
                    targetConstructorInfo = constructorInfo;
                    parameters = [model, Target];
                }
                else if (parameterInfos.Length == 1 && targetConstructorInfo is null && parameterInfos[0].ParameterType == typeof(LoggerWriterModel))
                {
                    targetConstructorInfo = constructorInfo;
                    parameters = [model];
                }
                break;
            }
            if (targetConstructorInfo is null || parameters is null) throw new LoggerException($"未找到{modelType.Name}合适的构造函数");
            TModel data = (TModel)targetConstructorInfo.Invoke(parameters);
            await WriteLoggerAsync(data);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract Task WriteLoggerAsync(TModel model);
    }
}
