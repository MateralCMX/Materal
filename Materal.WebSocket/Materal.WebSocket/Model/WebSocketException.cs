using Materal.Common;
using System;

namespace Materal.WebSocket.Model
{
    public class MateralWebSocketException : MateralException
    {
        public MateralWebSocketException()
        {
        }

        public MateralWebSocketException(string message) : base(message)
        {
        }

        public MateralWebSocketException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
