using Materal.WebSocket.Events.Model;
using System;

namespace TestClient.Events
{
    public class TestClientEventException : EventException
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
