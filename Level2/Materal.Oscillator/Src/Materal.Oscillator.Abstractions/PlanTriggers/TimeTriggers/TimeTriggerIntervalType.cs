using System.ComponentModel;

namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间触发器间隔类型
    /// </summary>
    public enum TimeTriggerIntervalType
    {
        /// <summary>
        /// 小时
        /// </summary>
        [Description("小时")]
        Hour = 0,
        /// <summary>
        /// 分钟
        /// </summary>
        [Description("分钟")]
        Minute = 1,
        /// <summary>
        /// 秒
        /// </summary>
        [Description("秒")]
        Second = 2
    }
}
