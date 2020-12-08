using Demo.Common;

namespace Demo.Commands
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendMessageCommand : DemoCommand
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message{ get; set; }
    }
}
