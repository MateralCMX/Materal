using Materal.Common;

namespace Materal.Gateway.Common
{
    public class MateralGatewayException : MateralException
    {
        public MateralGatewayException() { }
        public MateralGatewayException(string message) : base(message) { }
        public MateralGatewayException(string message, Exception innerException) : base(message, innerException) { }
    }
}
