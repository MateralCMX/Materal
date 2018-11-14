using Materal.WebSocket.Events.Model;
using System;

namespace TestClient.Events
{
    public class TestClientEventException : MateralWebSocketEventException
    {
        public TestClientEventException()
        {
        }

        public TestClientEventException(string message) : base(message)
        {
        }

        public TestClientEventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
