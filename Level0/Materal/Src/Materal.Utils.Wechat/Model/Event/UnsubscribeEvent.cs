namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 取消关注公众号事件
    /// </summary>
    public class UnsubscribeEvent(XmlDocument xmlDocument) : WechatServerEvent(xmlDocument)
    {
    }
}
