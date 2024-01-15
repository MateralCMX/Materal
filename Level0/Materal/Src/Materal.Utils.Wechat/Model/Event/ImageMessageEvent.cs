namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 图片消息事件
    /// </summary>
    public class ImageMessageEvent : WechatServerEvent
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// 消息唯一标识
        /// </summary>
        public string MessageID { get; set; } = string.Empty;
        /// <summary>
        /// 消息数据唯一标识
        /// </summary>
        public string MessageDataID { get; set; } = string.Empty;
        /// <summary>
        /// 消息媒体唯一标识
        /// </summary>
        public string MediaID { get; set; } = string.Empty;
        /// <summary>
        /// 图片Url
        /// </summary>
        public string PicUrl { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public ImageMessageEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
            MediaID = GetXmlValue("MediaId");
            PicUrl = GetXmlValue(nameof(PicUrl));
            ID = GetXmlValue("Idx");
            MessageID = GetXmlValue("MsgId");
            MessageDataID = GetXmlValue("MsgDataId");
        }
    }
}
