using System.Xml;

namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 语音消息事件
    /// </summary>
    public class VoiceMessageEvent : WechatServerEvent
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; } = string.Empty;
        /// <summary>
        /// 订阅用户的OpenID
        /// </summary>
        public string FromUserName { get; set; } = string.Empty;
        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime CreateTime { get; set; }
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
        /// 格式
        /// </summary>
        public string Format { get; set; } = string.Empty;
        /// <summary>
        /// 语言识别结果
        /// </summary>
        public string Recognition { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public VoiceMessageEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
            ToUserName = GetXmlValue(nameof(ToUserName));
            FromUserName = GetXmlValue(nameof(FromUserName));
            CreateTime = GetXmlValueForDateTime(nameof(CreateTime));
            MediaID = GetXmlValue("MediaId");
            Format = GetXmlValue(nameof(Format));
            Recognition = GetXmlValue(nameof(Recognition));
            MessageID = GetXmlValue("MsgId");
            MessageDataID = GetXmlValue("MsgDataId");
            ID = GetXmlValue("Idx");
        }
    }
}
