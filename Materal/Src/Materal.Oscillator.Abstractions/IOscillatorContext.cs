using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器上下文
    /// </summary>
    public interface IOscillatorContext
    {
        /// <summary>
        /// 调度器数据
        /// </summary>
        IOscillatorData OscillatorData { get; }
        /// <summary>
        /// 任务数据
        /// </summary>
        IWorkData WorkData { get; }
        /// <summary>
        /// 执行的计划触发器数据
        /// </summary>
        IPlanTriggerData PlanTriggerData { get; }
        /// <summary>
        /// Quartz触发器
        /// </summary>
        ITrigger Trigger { get; }
        /// <summary>
        /// 执行成功标识
        /// </summary>
        bool IsSuccess { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        Exception? Exception { get; set; }
        /// <summary>
        /// 任务异常
        /// </summary>
        Exception? WorkException { get; set; }
        /// <summary>
        /// 成功任务异常
        /// </summary>
        Exception? SuccessWorkException { get; set; }
        /// <summary>
        /// 失败任务异常
        /// </summary>
        Exception? FailWorkException { get; set; }
        /// <summary>
        /// 上下文数据
        /// </summary>
        Dictionary<string, object?> ContextData { get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; set; }
        /// <summary>
        /// 耗时
        /// </summary>
        long ElapsedTime { get; set; }
        /// <summary>
        /// 调度器监听器
        /// </summary>
        List<IOscillatorListener> Listeners { get; }
    }
}
