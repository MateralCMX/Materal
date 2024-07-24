using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 任务上下文
    /// </summary>
    public class WorkContext(IServiceProvider serviceProvider, IOscillator oscillator, Dictionary<string, object?> loggerScopeData, string triggerName) : IWorkContext
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; } = serviceProvider;
        /// <summary>
        /// 日志作用域
        /// </summary>
        public Dictionary<string, object?> LoggerScopeData { get; } = loggerScopeData;
        /// <summary>
        /// 异常
        /// </summary>
        public string? Exception { get; set; }
        /// <summary>
        /// 调度器对象
        /// </summary>
        public IOscillator Oscillator { get; } = oscillator;
        /// <summary>
        /// 计划触发器唯一标识
        /// </summary>
        public Guid? PlanTriggerID { get; set; }
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName { get; set; } = triggerName;
    }
}
