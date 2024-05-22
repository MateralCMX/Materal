using Materal.Logger.Abstractions;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务上下文
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 日志作用域
        /// </summary>
        LoggerScope LoggerScope { get; }
        /// <summary>
        /// 异常
        /// </summary>
        Exception? Exception { get; set; }
        /// <summary>
        /// 调度器对象
        /// </summary>
        IOscillator Oscillator { get; }
        /// <summary>
        /// 计划触发器唯一标识
        /// </summary>
        Guid? PlanTriggerID { get; set; }
    }
}
