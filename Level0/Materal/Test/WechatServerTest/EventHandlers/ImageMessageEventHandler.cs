using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class ImageMessageEventHandler : IImageMessageEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(ImageMessageEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}发送了图片消息:{@event.MediaID}");
            return Task.FromResult((ReplyMessageModel?)null);
        }
    }
}
