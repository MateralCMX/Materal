using Materal.WebSocket.Model;
using System;

namespace Materal.WebSocket.Commands.Model
{
    public class CommandException : MateralWebSocketException
    {
        public CommandException()
        {
        }

        public CommandException(string message) : base(message)
        {
        }

        public CommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
