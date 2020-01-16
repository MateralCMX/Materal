using System;

namespace Materal.DotNetty.Server.Core
{
    public class DotNettyServerException : Exception
    {
        public DotNettyServerException()
        {
        }

        public DotNettyServerException(string message) : base(message)
        {
        }

        public DotNettyServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
