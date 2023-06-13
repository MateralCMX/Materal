using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Event;
using Materal.Utils.Wechat.ServerEventHandler;

namespace WechatServerTest.EventHandlers
{
    public class TextMessageEventHandler : ITextMessageEventHandler
    {
        public Task<ReplyMessageModel?> HandlerAsync(TextMessageEvent @event)
        {
            Console.WriteLine($"用户{@event.FromUserName}发送了文本消息:{@event.Content}");
            ReplyMessageModel result;
            if (@event.Content.Trim().StartsWith("教我"))
            {
                result = new ReplyTextMessageModel(@event.FromUserName, @event.ToUserName, "你问我？文档就写在那里不会看啊，不会就问百度啊，还不行就上谷歌，怎么魔法？草泥马！食屎啦你！");
            }
            else
            {
                result = new ReplyTextMessageModel(@event.FromUserName, @event.ToUserName, "听不懂你在说啥，你可试试：教我XXX");
            }
            return Task.FromResult((ReplyMessageModel?)result);
        }
    }
}
