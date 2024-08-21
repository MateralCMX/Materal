using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class UnsubscribeEventHandler : IUnsubscribeEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(UnsubscribeEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}在{@event.CreateTime}取消关注了公众号");
            return Task.FromResult((ReplyMessageModel?)null);
        }
    }
}
