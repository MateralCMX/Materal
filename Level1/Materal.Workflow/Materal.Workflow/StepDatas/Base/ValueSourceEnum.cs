using System.ComponentModel;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 值来源枚举
    /// </summary>
    public enum ValueSourceEnum : byte
    {
        /// <summary>
        /// 运行时数据属性
        /// </summary>
        [Description("运行时数据属性")]
        RuntimeDataProperty = 0,
        /// <summary>
        /// 节点数据属性
        /// </summary>
        [Description("节点数据属性")]
        BuildDataProperty = 1,
        /// <summary>
        /// 常量
        /// </summary>
        [Description("常量")]
        Constant = 2
    }
}
