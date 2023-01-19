namespace Materal.WeChatHelper.Model
{
    public class NetworkDetectionPingModel
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public NetworkDetectionOperatorEnum FromOperator { get; set; }
        /// <summary>
        /// 丢包率
        /// </summary>
        public string PackageLoss { get; set; }
        /// <summary>
        /// 用时
        /// </summary>
        public string Time { get; set; }
    }
}
