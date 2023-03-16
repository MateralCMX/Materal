using System.ComponentModel;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 比较类型
    /// </summary>
    public enum ComparisonTypeEnum : byte
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal = 0,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEqual = 1,
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GreaterThan = 2,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LessThan = 3,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterThanOrEqual = 4,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessThanOrEqual = 5
    }
}
