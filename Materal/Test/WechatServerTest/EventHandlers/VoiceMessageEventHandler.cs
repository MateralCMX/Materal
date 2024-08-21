using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class VoiceMessageEventHandler : IVoiceMessageEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(VoiceMessageEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}发送了语音消息:{@event.Recognition}");
            return Task.FromResult((ReplyMessageModel?)null);
        }
    }
}
