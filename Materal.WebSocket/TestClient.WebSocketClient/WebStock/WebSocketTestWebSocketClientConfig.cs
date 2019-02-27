using System.Text;
using Materal.WebSocket.Client.Model;

namespace TestClient.WebSocketClient.WebStock
{
    public class WebSocketTestWebSocketClientConfig : WebSocketClientConfig, ITestWebSocketClientConfig
    {
        public WebSocketTestWebSocketClientConfig()
        {
            Url = "ws://127.0.0.1:8080";
            EncodingType = Encoding.UTF8;
            ServerMessageMaxLength = 102400;
        }
    }
}
