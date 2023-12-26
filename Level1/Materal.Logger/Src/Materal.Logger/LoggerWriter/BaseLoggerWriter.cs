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
            if (!rule.Enable) return false;
            if (model.LogLevel < rule.MinLogLevel || model.LogLevel > rule.MaxLogLevel) return false;
            return true;
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
            object? dataObj = typeof(TModel).Instantiation(model);
            if (dataObj is null || dataObj is not TModel data) return;
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
