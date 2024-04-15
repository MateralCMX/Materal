namespace Materal.Logger
{
    /// <summary>
    /// 日志写入器基类
    /// </summary>
    /// <typeparam name="TLoggerTargetOptions"></typeparam>
    public abstract class BaseLoggerWriter<TLoggerTargetOptions> : ILoggerWriter, ILoggerWriter<TLoggerTargetOptions>
        where TLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        public virtual async Task LogAsync(Log log, LoggerRuleOptions ruleOptions, LoggerTargetOptions targetOptions)
        {
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
        public virtual async Task<bool> CanLogAsync(Log log, LoggerRuleOptions ruleOptions, LoggerTargetOptions targetOptions)
        {
            if (!targetOptions.Enable) return false;
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
        public virtual async Task<bool> CanLogAsync(Log log, LoggerRuleOptions ruleOptions, TLoggerTargetOptions targetOptions) => await Task.FromResult(true);
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="hostLogger"></param>
        /// <returns></returns>
        public virtual async Task StartAsync(IHostLogger hostLogger)
        {
            WriteStartInfo(hostLogger);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写启动信息
        /// </summary>
        /// <param name="hostLogger"></param>
        protected virtual void WriteStartInfo(IHostLogger hostLogger)
        {
            string name = GetType().Name;
            hostLogger.LogDebug($"正在启动{name}");
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="hostLogger"></param>
        /// <returns></returns>
        public virtual async Task StopAsync(IHostLogger hostLogger)
        {
            WriteStopInfo(hostLogger);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写关闭信息
        /// </summary>
        /// <param name="hostLogger"></param>
        protected virtual void WriteStopInfo(IHostLogger hostLogger)
        {
            string name = GetType().Name;
            hostLogger.LogDebug($"正在关闭{name}");
        }
    }
}
