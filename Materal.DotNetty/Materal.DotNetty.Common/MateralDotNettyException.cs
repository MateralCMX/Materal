using Materal.Common;
using System;

namespace Materal.DotNetty.Common
{
    public class MateralDotNettyException : MateralException
    {
        public MateralDotNettyException()
        {
        }

        public MateralDotNettyException(string message) : base(message)
        {
        }

        public MateralDotNettyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
