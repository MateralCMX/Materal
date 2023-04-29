using System.ComponentModel;

namespace Materal.Oscillator.PlanTriggers
{
    public enum EveryDayIntervalType
    {
        [Description("小时")]
        Hour = 0,
        [Description("分钟")]
        Minute = 1,
        [Description("秒")]
        Second = 2
    }
}
