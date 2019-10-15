using System;

namespace Materal.ConDep.Events
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
        /// 消息
        /// </summary>
        public string Message { get; set; } = "服务器错误，请联系后端工程师。";
    }
}
