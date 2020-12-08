using Demo.Common;

namespace Demo.Events
{
    /// <summary>
    /// 回复消息事件
    /// </summary>
    public class ReplyMessageEvent : DemoEvent
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
