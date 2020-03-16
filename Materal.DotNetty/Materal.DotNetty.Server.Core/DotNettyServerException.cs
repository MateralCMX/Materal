using System;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.Server.Core
{
    public class DotNettyServerException : DotNettyException
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
