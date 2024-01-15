namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 微信服务事件
    /// </summary>
    public abstract class WechatServerEvent
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
        /// 事件发生时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 原始Xml
        /// </summary>
        public XmlDocument XmlDocument { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        protected WechatServerEvent(XmlDocument xmlDocument)
        {
            XmlDocument = xmlDocument;
            ToUserName = GetXmlValue(nameof(ToUserName));
            FromUserName = GetXmlValue(nameof(FromUserName));
            CreateTime = GetXmlValueForDateTime(nameof(CreateTime));
        }
        /// <summary>
        /// 获得Xml值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="WechatException"></exception>
        protected string GetXmlValue(string name)
        {
            if (XmlDocument.FirstChild is null) throw new WechatException("未识别xml文档");
            XmlNodeList? nodes = XmlDocument.FirstChild.SelectNodes(name);
            if (nodes is null || nodes.Count <= 0 || nodes[0] is null) return string.Empty;
            XmlNode? node = nodes[0];
            if (node is null) return string.Empty;
            return (node.FirstChild is not null ? node.FirstChild.Value : node.Value) ?? string.Empty;
        }
        /// <summary>
        /// 获得Xml值(时间格式)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected DateTime GetXmlValueForDateTime(string name)
        {
            string dateTimeString = GetXmlValue(name);
            if (string.IsNullOrWhiteSpace(dateTimeString)) throw new WechatException("时间值错误");
            long dateTimeSecond = Convert.ToInt64(dateTimeString);
            DateTime result = new(1970, 1, 1);
            result = result.AddHours(8).AddSeconds(dateTimeSecond);
            return result;
        }
    }
}
