using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class LinkMessageEventHandler : ILinkMessageEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(LinkMessageEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}发送了链接消息:{@event.Url}");
            return Task.FromResult((ReplyMessageModel?)null);
        }
    }
}
