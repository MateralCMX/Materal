using Materal.Common;
using System;

namespace Materal.NetworkHelper
{
    public class MateralNetworkException : MateralException
    {
        public MateralNetworkException()
        {
        }

        public MateralNetworkException(string message) : base(message)
        {
        }

        public MateralNetworkException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
