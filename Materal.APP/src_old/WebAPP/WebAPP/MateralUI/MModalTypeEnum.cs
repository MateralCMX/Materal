using WebAPP.MateralUI.Helper;

namespace WebAPP.MateralUI
{
    /// <summary>
    /// 模态窗类型
    /// </summary>
    public enum MModalTypeEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        [TargetClass(null)]
        Default = 0,
        /// <summary>
        /// 大号
        /// </summary>
        [TargetClass("m_modal_large")]
        Large = 1,
        /// <summary>
        /// 小号
        /// </summary>
        [TargetClass("m_modal_small")]
        Small = 1
    }
}
