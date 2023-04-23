using System.ComponentModel;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 网络检测供应商枚举
    /// </summary>
    public enum NetworkDetectionOperatorEnum
    {
        /// <summary>
        /// 中国电信
        /// </summary>
        [Description("中国电信")]
        ChinaNet,
        /// <summary>
        /// 中国联通
        /// </summary>
        [Description("中国联通")]
        UniCom,
        /// <summary>
        /// 腾讯
        /// </summary>
        [Description("腾讯")]
        Cap,
        /// <summary>
        /// 根据IP选择
        /// </summary>
        [Description("根据IP选择")]
        Default
    }
}
