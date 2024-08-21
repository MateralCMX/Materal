namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 地理位置消息事件
    /// </summary>
    public class LocationMessageEvent : WechatServerEvent
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
        /// 纬度
        /// </summary>
        public string Location_X { get; set; } = string.Empty;
        /// <summary>
        /// 经度
        /// </summary>
        public string Location_Y { get; set; } = string.Empty;
        /// <summary>
        /// 缩放大小
        /// </summary>
        public string Scale { get; set; } = string.Empty;
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public LocationMessageEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
            Location_X = GetXmlValue(nameof(Location_X));
            Location_Y = GetXmlValue(nameof(Location_Y));
            Scale = GetXmlValue(nameof(Scale));
            Label = GetXmlValue(nameof(Label));
            MessageID = GetXmlValue("MsgId");
            MessageDataID = GetXmlValue("MsgDataId");
            ID = GetXmlValue("Idx");
        }
    }
}
