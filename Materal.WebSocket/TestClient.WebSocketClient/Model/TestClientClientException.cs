using Materal.WebSocket.Model;
using System;

namespace TestClient.WebStockClient.Model
{
    public class TestClientClientException : ClientException
    {
        public TestClientClientException()
        {
        }

        public TestClientClientException(string message) : base(message)
        {
        }

        public TestClientClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
