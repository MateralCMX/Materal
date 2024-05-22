using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器
    /// </summary>
    public interface IOscillator
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// 作业明细
        /// </summary>
        IWorkData WorkData { get; set; }
        /// <summary>
        /// 触发器组
        /// </summary>
        List<IPlanTrigger> Triggers { get; set; }
    }
}
