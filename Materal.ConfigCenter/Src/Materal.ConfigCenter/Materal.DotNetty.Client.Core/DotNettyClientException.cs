using System;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.Client.Core
{
    public class DotNettyClientException : DotNettyException
    {
        public DotNettyClientException()
        {
        }

        public DotNettyClientException(string message) : base(message)
        {
        }

        public DotNettyClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
