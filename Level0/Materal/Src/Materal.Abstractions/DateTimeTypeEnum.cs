namespace Materal.Abstractions
{
    /// <summary>
    /// 日期时间类型枚举
    /// </summary>
    public enum DateTimeTypeEnum
    {
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year = 0,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month = 1,
        /// <summary>
        /// 日
        /// </summary>
        [Description("日")]
        Day = 2,
        /// <summary>
        /// 时
        /// </summary>
        [Description("时")]
        Hour = 3,
        /// <summary>
        /// 分
        /// </summary>
        [Description("分")]
        Minute = 4,
        /// <summary>
        /// 秒
        /// </summary>
        [Description("秒")]
        Second = 5,
        /// <summary>
        /// 毫秒
        /// </summary>
        [Description("毫秒")]
        Millisecond = 6
    }
}
