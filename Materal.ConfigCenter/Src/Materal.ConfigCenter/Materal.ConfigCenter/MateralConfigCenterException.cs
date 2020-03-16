using System;
using Materal.Common;

namespace Materal.ConfigCenter
{
    public class MateralConfigCenterException : MateralException
    {
        public MateralConfigCenterException()
        {
        }

        public MateralConfigCenterException(string message) : base(message)
        {
        }

        public MateralConfigCenterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
