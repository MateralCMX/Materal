using System;
using Materal.Common;

namespace Materal.Gateway.Common.Models
{
    public class GatewayException : MateralException
    {
        public GatewayException()
        {
        }

        public GatewayException(string message)
            : base(message)
        {
        }

        public GatewayException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
