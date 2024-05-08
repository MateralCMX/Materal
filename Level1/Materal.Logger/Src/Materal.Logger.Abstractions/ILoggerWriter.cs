namespace Materal.Logger.Abstractions
{
    /// <summary>
    /// 日志写入器
    /// </summary>
    public interface ILoggerWriter
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
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
