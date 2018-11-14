using Materal.WebSocket.Model;
using System;

namespace Materal.WebSocket.Events.Model
{
    public class EventException : WebStockException
    {
        public EventException()
        {
        }

        public EventException(string message) : base(message)
        {
        }

        public EventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
