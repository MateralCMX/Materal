using System.ComponentModel;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 条件类型枚举
    /// </summary>
    public enum ConditionEnum : byte
    {
        /// <summary>
        /// 并且
        /// </summary>
        [Description("并且")]
        And = 0,
        /// <summary>
        /// 或者
        /// </summary>
        [Description("或者")]
        Or = 1
    }
}
