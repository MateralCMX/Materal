using Materal.WebSocket.Commands.Model;
using System;

namespace TestClient.Commands
{
    public class TestClientCommandException : CommandException
    {
        public TestClientCommandException()
        {
        }

        public TestClientCommandException(string message) : base(message)
        {
        }

        public TestClientCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
