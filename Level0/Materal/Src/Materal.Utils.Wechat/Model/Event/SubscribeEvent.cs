using System.Xml;

namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 关注公众号事件
    /// </summary>
    public class SubscribeEvent : WechatServerEvent
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public SubscribeEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
        }
    }
}
