using System.ComponentModel;

namespace Materal.BusinessFlow.Abstractions.Enums
{
    public enum FlowStateEnum : byte
    {
        /// <summary>
        /// 执行中
        /// </summary>
        [Description("执行中")]
        Running = 0,
        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        End = 1
    }
}
