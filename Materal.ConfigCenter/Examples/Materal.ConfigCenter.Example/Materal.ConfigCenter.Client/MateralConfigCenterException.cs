using System;

namespace Materal.ConfigCenter.Client
{
    public class MateralConfigCenterClientException : MateralConfigCenterException
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
