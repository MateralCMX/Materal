using System;

namespace Materal.MicroFront.Events
{
    public class ServerErrorEvent : Event
    {
        public ServerErrorEvent()
        {
        }
        public ServerErrorEvent(Exception ex)
        {
            Message = ex.Message;
        }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; } = 500;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "服务器错误，请联系后端工程师。";
    }
}
