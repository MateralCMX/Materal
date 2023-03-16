using System.ComponentModel;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 输入值来源枚举
    /// </summary>
    public enum InputValueSourceEnum : byte
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
        Constant = 2,
        /// <summary>
        /// 输入数据
        /// </summary>
        [Description("输入数据")]
        InputData = 3
    }
}
