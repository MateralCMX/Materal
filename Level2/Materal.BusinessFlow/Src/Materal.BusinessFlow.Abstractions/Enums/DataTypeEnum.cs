using System.ComponentModel;

namespace Materal.BusinessFlow.Abstractions
{
    /// <summary>
    /// 数据类型枚举
    /// </summary>
    public enum DataTypeEnum : byte
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [Description("字符串")]
        String = 0,
        /// <summary>
        /// 数字
        /// </summary>
        [Description("数字")]
        Number = 1,
        /// <summary>
        /// 日期时间
        /// </summary>
        [Description("日期时间")]
        DateTime = 2,
        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        Date = 3,
        /// <summary>
        /// 时间
        /// </summary>
        [Description("时间")]
        Time = 4,
        /// <summary>
        /// 布尔值
        /// </summary>
        [Description("布尔")]
        Boole = 5,
        /// <summary>
        /// 枚举
        /// </summary>
        [Description("枚举")]
        Enum = 6
    }
}
