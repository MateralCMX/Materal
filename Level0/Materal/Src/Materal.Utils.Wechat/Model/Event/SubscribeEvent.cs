namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 关注公众号事件
    /// </summary>
    public class SubscribeEvent(XmlDocument xmlDocument) : WechatServerEvent(xmlDocument)
    {
    }
}
