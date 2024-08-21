using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class LocationMessageEventHandler : ILocationMessageEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(LocationMessageEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}发送了位置消息:{@event.Label}[{@event.Location_Y},{@event.Location_X}]");
            return Task.FromResult((ReplyMessageModel?)null);
        }
    }
}
