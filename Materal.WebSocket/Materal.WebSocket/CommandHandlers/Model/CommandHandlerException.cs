using Materal.WebSocket.Model;
using System;

namespace Materal.WebSocket.CommandHandlers.Model
{
    public class CommandHandlerException : MateralWebSocketException
    {
        public CommandHandlerException()
        {
        }

        public CommandHandlerException(string message) : base(message)
        {
        }

        public CommandHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
