﻿using Materal.Utils.Wechat.Model.Event;

namespace Materal.Utils.Wechat.ServerEventHandler
{
    /// <summary>
    /// 文本消息事件处理器
    /// </summary>
    public interface ITextMessageEventHandler : IEventHandler<TextMessageEvent>
    {
    }
}
