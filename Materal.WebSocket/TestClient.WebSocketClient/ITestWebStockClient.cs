using System.Threading.Tasks;
using Materal.WebSocket;
using TestClient.Events;

namespace TestClient.WebSocketClient
{
    public interface ITestWebSocketClient : IWebSocketClient
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="eventM">消息</param>
        /// <returns></returns>
        Task HandleEventAsync(Event eventM);
    }
}
