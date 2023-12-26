namespace Materal.Logger.LoggerHandlers
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
        protected readonly TTarget TargetConfig = target;
        /// <summary>
        /// 是否关闭
        /// </summary>
        protected bool IsClose { get; set; } = false;
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public virtual async Task ShutdownAsync()
        {
            IsClose = true;
            LoggerHost.LoggerLog?.LogDebug($"正在关闭[{TargetConfig.Name}]");
            await Task.CompletedTask;
            LoggerHost.LoggerLog?.LogDebug($"[{TargetConfig.Name}]关闭成功");
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
            if (!TargetConfig.Enable) return false;
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
            if (!rule.Targets.Contains(TargetConfig.Name)) return false;
            if (rule.Scopes is not null && rule.Scopes.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(model.Scope.ScopeName) || !rule.Scopes.Contains(model.Scope.ScopeName)) return false;
            }
            if (rule.LogLevel is not null && rule.LogLevel.Count > 0) return CanWriteLogger(model, rule, rule.LogLevel);
            else if (model.Config.LogLevel is not null && model.Config.LogLevel.Count > 0) return CanWriteLogger(model, rule, model.Config.LogLevel);
            else if (LoggerConfig.DefaultLogLevel is not null && LoggerConfig.DefaultLogLevel.Count > 0) return CanWriteLogger(model, rule, LoggerConfig.DefaultLogLevel);
            else return true;
        }
        /// <summary>
        /// 是否可以写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rule"></param>
        /// <param name="logLevels"></param>
        /// <returns></returns>
        protected virtual bool CanWriteLogger(LoggerWriterModel model, RuleConfig rule, Dictionary<string, LogLevel>? logLevels)
        {
            if (logLevels is null) return true;
            LogLevel? configLogLevel = null;
            if (model.CategoryName is not null && !string.IsNullOrWhiteSpace(model.CategoryName))
            {
                int index = 0;
                foreach (string key in logLevels.Keys)
                {
                    if (model.CategoryName == key)
                    {
                        configLogLevel = logLevels[key];
                        break;
                    }
                    if (!model.CategoryName.StartsWith(key)) continue;
                    int nowIndex = key.Split('.').Length;
                    if (index > nowIndex) continue;
                    index = nowIndex;
                    configLogLevel = logLevels[key];
                }
            }
            if (configLogLevel is null && logLevels.TryGetValue("Default", out LogLevel value))
            {
                configLogLevel = value;
            }
            if (configLogLevel is null || configLogLevel == LogLevel.None) return false;
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
                    parameters = [model, TargetConfig];
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
