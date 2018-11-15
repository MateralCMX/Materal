namespace Materal.WebSocket.Client.Model
{
    public interface IWebSocketClientHandler
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        void OnSendMessage(object message);
    }
}
