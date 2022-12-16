using Materal.Common;

namespace ConfigCenter.Client
{
    public class MateralConfigCenterClientException : MateralException
    {
        public MateralConfigCenterClientException()
        {
        }
        public MateralConfigCenterClientException(string message) : base(message)
        {
        }
        public MateralConfigCenterClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
