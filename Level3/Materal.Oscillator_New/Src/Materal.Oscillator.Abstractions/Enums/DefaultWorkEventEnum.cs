using System.ComponentModel;

namespace Materal.Oscillator.Abstractions.Enums
{
    /// <summary>
    /// 默认任务事件枚举
    /// </summary>
    public enum DefaultWorkEventEnum
    {
        /// <summary>
        /// 下一个任务
        /// </summary>
        [Description("下一个任务")]
        Next = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 2,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete = 3
    }
}
