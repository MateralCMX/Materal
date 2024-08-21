namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 链接消息事件
    /// </summary>
    public class LinkMessageEvent : WechatServerEvent
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
        /// 消息标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public LinkMessageEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
            Title = GetXmlValue(nameof(Title));
            Description = GetXmlValue(nameof(Description));
            Url = GetXmlValue(nameof(Url));
            ID = GetXmlValue("Idx");
            MessageID = GetXmlValue("MsgId");
            MessageDataID = GetXmlValue("MsgDataId");
        }
    }
}
