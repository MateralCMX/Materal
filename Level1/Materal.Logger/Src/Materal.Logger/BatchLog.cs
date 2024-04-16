namespace Materal.Logger
{
    /// <summary>
    /// 批量日志模型
    /// </summary>
    /// <typeparam name="TLoggerTargetOptions"></typeparam>
    public class BatchLog<TLoggerTargetOptions>(Log log, LoggerRuleOptions ruleOptions, TLoggerTargetOptions targetOptions)
        where TLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 日志
        /// </summary>
        public Log Log { get; } = log;
        /// <summary>
        /// 规则选项
        /// </summary>
        public LoggerRuleOptions RuleOptions { get; } = ruleOptions;
        /// <summary>
        /// 目标选项
        /// </summary>
        public TLoggerTargetOptions TargetOptions { get; } = targetOptions;        
    }
}
