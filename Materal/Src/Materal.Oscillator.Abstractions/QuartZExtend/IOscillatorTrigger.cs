using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    /// <summary>
    /// Oscillator触发器
    /// </summary>
    public interface IOscillatorTrigger : ITrigger, IComparable<ITrigger>
    {
        /// <summary>
        /// 重复次数,-1为永远重复
        /// </summary>
        int RepeatCount { get; set; }
        /// <summary>
        /// 重复间隔
        /// </summary>
        TimeSpan RepeatInterval { get; set; }
        /// <summary>
        /// 时间触发
        /// </summary>
        int TimesTriggered { get; set; }
        /// <summary>
        /// 计划触发器数据
        /// </summary>
        IPlanTrigger? PlanTriggerData { get; set; }
    }
}
