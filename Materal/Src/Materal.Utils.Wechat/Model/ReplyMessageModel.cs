namespace Materal.Utils.Wechat.Model
{
    /// <summary>
    /// 回复消息模型
    /// </summary>
    public abstract class ReplyMessageModel
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; }
        /// <summary>
        /// 订阅用户的OpenID
        /// </summary>
        public string FromUserName { get; }
        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime CreateTime { get; } = DateTime.Now;
        /// <summary>
        /// 订阅时间
        /// </summary>
        public long CreateTimeStamp => CreateTime.AddHours(-8).GetTimeStamp();
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="messageType"></param>
        protected ReplyMessageModel(string toUserName, string fromUserName, string messageType)
        {
            MessageType = messageType;
            ToUserName = toUserName;
            FromUserName = fromUserName;
        }
        /// <summary>
        /// 获得XmlDocument
        /// </summary>
        /// <returns></returns>
        public abstract XmlDocument GetXmlDocument();
        /// <summary>
        /// 获得Xml字符串
        /// </summary>
        /// <returns></returns>
        public string GetXmlString() => GetXmlDocument().InnerXml;
    }
    /// <summary>
    /// 回复文本消息模型
    /// </summary>
    public class ReplyTextMessageModel(string toUserName, string fromUserName, string content) : ReplyMessageModel(toUserName, fromUserName, "text")
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; } = content;

        /// <summary>
        /// 获得Xml文档
        /// </summary>
        /// <returns></returns>
        public override XmlDocument GetXmlDocument()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"<xml>");
            stringBuilder.Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>");
            stringBuilder.Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>");
            stringBuilder.Append($"<CreateTime>{CreateTimeStamp}</CreateTime>");
            stringBuilder.Append($"<MsgType><![CDATA[{MessageType}]]></MsgType>");
            stringBuilder.Append($"<Content><![CDATA[{Content}]]></Content>");
            stringBuilder.Append($"</xml>");
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(stringBuilder.ToString());
            return xmlDocument;
        }
    }
}
