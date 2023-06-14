using System.Xml;

namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 取消关注公众号事件
    /// </summary>
    public class UnsubscribeEvent : WechatServerEvent
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public UnsubscribeEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
        }
    }
}
