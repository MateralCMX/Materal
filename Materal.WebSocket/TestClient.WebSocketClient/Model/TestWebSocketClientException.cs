using Materal.WebSocket.Model;
using System;

namespace TestClient.WebSocketClient.Model
{
    public class TestWebSocketClientException : MateralWebSocketClientException
    {
        public TestWebSocketClientException()
        {
        }

        public TestWebSocketClientException(string message) : base(message)
        {
        }

        public TestWebSocketClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
