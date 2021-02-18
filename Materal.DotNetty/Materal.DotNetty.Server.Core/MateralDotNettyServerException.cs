using System;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.Server.Core
{
    public class MateralDotNettyServerException : MateralDotNettyException
    {
        public MateralDotNettyServerException()
        {
        }

        public MateralDotNettyServerException(string message) : base(message)
        {
        }

        public MateralDotNettyServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
