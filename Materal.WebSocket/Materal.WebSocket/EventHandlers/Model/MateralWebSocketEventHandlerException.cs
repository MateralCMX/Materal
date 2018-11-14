using Materal.WebSocket.Model;
using System;

namespace Materal.WebSocket.EventHandlers.Model
{
    public class MateralWebSocketEventHandlerException : MateralWebSocketException
    {
        public MateralWebSocketEventHandlerException()
        {
        }

        public MateralWebSocketEventHandlerException(string message) : base(message)
        {
        }

        public MateralWebSocketEventHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
