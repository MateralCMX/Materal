using Materal.Abstractions;

namespace Materal.Gateway.Common
{
    public class GatewayException : MateralException
    {
        public GatewayException() { }
        public GatewayException(string message) : base(message) { }
        public GatewayException(string message, Exception innerException) : base(message, innerException) { }
    }
}
