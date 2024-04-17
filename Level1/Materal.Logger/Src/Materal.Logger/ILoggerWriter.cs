namespace Materal.Logger
{
    /// <summary>
    /// 日志写入器
    /// </summary>
    public interface ILoggerWriter
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="hostLogger"></param>
        /// <returns></returns>
        Task StartAsync(IHostLogger hostLogger);
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="hostLogger"></param>
        /// <returns></returns>
        Task StopAsync(IHostLogger hostLogger);
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        Task LogAsync(Log log, LoggerRuleOptions ruleOptions, LoggerTargetOptions targetOptions);
    }
    /// <summary>
    /// 日志写入器
    /// </summary>
    public interface ILoggerWriter<TLoggerTargetOptions>
        where TLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        Task LogAsync(Log log, LoggerRuleOptions ruleOptions, TLoggerTargetOptions targetOptions);
    }
}
