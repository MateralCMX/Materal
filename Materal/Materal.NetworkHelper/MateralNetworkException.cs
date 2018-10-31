using Materal.Common;
using System;
using System.Runtime.Serialization;

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

        protected MateralNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
