using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class VideoMessageEventHandler : IVideoMessageEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(VideoMessageEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}发送了视频消息:{@event.MediaID}");
            return Task.FromResult((ReplyMessageModel?)null);
        }
    }
}
