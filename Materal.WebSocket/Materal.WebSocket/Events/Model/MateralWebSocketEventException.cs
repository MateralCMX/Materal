using Materal.WebSocket.Model;
using System;

namespace Materal.WebSocket.Events.Model
{
    public class MateralWebSocketEventException : MateralWebSocketException
    {
        public MateralWebSocketEventException()
        {
        }

        public MateralWebSocketEventException(string message) : base(message)
        {
        }

        public MateralWebSocketEventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
