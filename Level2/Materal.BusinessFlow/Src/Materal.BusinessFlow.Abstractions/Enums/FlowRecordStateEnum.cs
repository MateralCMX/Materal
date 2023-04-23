using System.ComponentModel;

namespace Materal.BusinessFlow.Abstractions.Enums
{
    public enum FlowRecordStateEnum
    {
        /// <summary>
        /// 等待
        /// </summary>
        [Description("等待")]
        Wait = 0,
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
        /// 打回
        /// </summary>
        [Description("打回")]
        Repulse = 3,
    }
}
