using System.ComponentModel;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每天间隔类型
    /// </summary>
    public enum EveryDayIntervalType
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
