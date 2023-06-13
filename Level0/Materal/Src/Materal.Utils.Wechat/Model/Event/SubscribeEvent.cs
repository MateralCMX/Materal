using System.Xml;

namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 关注公众号事件
    /// </summary>
    public class SubscribeEvent : WechatServerEvent
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
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public SubscribeEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
            ToUserName = GetXmlValue(nameof(ToUserName));
            FromUserName = GetXmlValue(nameof(FromUserName));
            CreateTime = GetXmlValueForDateTime(nameof(CreateTime));
        }
    }
}
