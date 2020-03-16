using Materal.Common;
using System;

namespace Materal.DotNetty.Common
{
    public class DotNettyException : MateralException
    {
        public DotNettyException()
        {
        }

        public DotNettyException(string message) : base(message)
        {
        }

        public DotNettyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
