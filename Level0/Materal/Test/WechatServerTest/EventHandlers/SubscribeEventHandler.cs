using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class SubscribeEventHandler : ISubscribeEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(SubscribeEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}在{@event.CreateTime}关注了公众号");
            ReplyMessageModel result = new ReplyTextMessageModel(@event.FromUserName, @event.ToUserName, "你好啊靓仔~~~~");
            return Task.FromResult((ReplyMessageModel?)result);
        }
    }
}
