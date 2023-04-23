namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 网络检测枚举
    /// </summary>
    public enum NetworkDetectionActionEnum
    {
        /// <summary>
        /// 做域名解析
        /// </summary>
        DNS,
        /// <summary>
        /// 做Ping检测
        /// </summary>
        Ping,
        /// <summary>
        /// 做域名解析和Ping检测
        /// </summary>
        All
    }
}
