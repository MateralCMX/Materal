using WebAPP.MateralUI.Helper;

namespace WebAPP.MateralUI
{
    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum MButtonTypeEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        [TargetClass(null)]
        Default = 0,
        /// <summary>
        /// 主要
        /// </summary>
        [TargetClass("m_button_primary")]
        Primary = 1,
        /// <summary>
        /// 成功
        /// </summary>
        [TargetClass("m_button_success")]
        Success = 2,
        /// <summary>
        /// 警告
        /// </summary>
        [TargetClass("m_button_warning")]
        Warning = 3,
        /// <summary>
        /// 危险
        /// </summary>
        [TargetClass("m_button_danger")]
        Danger = 4
    }
}
