using System.ComponentModel;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 错误处理类型枚举
    /// </summary>
    public enum ErrorHandlerTypeEnum : byte
    {
        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stop = 0,
        /// <summary>
        /// 重试
        /// </summary>
        [Description("重试")]
        Retry = 1,
        /// <summary>
        /// 继续
        /// </summary>
        [Description("继续")]
        Next = 2,
    }
}
