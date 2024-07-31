using System.ComponentModel;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 月度
    /// </summary>
    public enum MonthFrequencyIndexType
    {
        /// <summary>
        /// 天
        /// </summary>
        [Description("天")]
        Day = 0,
        /// <summary>
        /// 星期天
        /// </summary>
        [Description("星期天")]
        Sunday = 1,
        /// <summary>
        /// 星期一
        /// </summary>
        [Description("星期一")]
        Monday = 2,
        /// <summary>
        /// 星期二
        /// </summary>
        [Description("星期二")]
        Tuesday = 3,
        /// <summary>
        /// 星期三
        /// </summary>
        [Description("星期三")]
        Wednesday = 4,
        /// <summary>
        /// 星期四
        /// </summary>
        [Description("星期四")]
        Thursday = 5,
        /// <summary>
        /// 星期五
        /// </summary>
        [Description("星期五")]
        Friday = 6,
        /// <summary>
        /// 星期六
        /// </summary>
        [Description("星期六")]
        Saturday = 7
    }
}
