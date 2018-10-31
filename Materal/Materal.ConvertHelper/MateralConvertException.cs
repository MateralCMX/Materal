using System;
using System.Runtime.Serialization;
using Materal.Common;

namespace Materal.ConvertHelper
{
    public class MateralConvertException : MateralException
    {
        public MateralConvertException()
        {
        }

        public MateralConvertException(string message) : base(message)
        {
        }

        public MateralConvertException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MateralConvertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
