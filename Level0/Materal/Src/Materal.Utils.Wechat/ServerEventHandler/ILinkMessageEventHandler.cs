using Materal.Utils.Wechat.Model.Event;

namespace Materal.Utils.Wechat.ServerEventHandler
{
    /// <summary>
    /// 链接消息事件处理器
    /// </summary>
    public interface ILinkMessageEventHandler : IEventHandler<LinkMessageEvent>
    {
    }
}
