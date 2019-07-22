using System.Text;
using Materal.WebSocket.Client.Model;

namespace TestClient.WebSocketClient.WebStock
{
    public class WebSocketTestWebSocketClientConfig : WebSocketClientConfig, ITestWebSocketClientConfig
    {
        public WebSocketTestWebSocketClientConfig()
        {
            Url = "ws://192.168.0.148:8080";
            EncodingType = Encoding.UTF8;
            ServerMessageMaxLength = 102400;
        }
    }
}
