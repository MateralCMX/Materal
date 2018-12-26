using Materal.Common;
using System;
using System.Runtime.Serialization;

namespace Materal.EnumHelper
{
    public class MateralEnumHelperException : MateralException
    {
        public MateralEnumHelperException()
        {
        }

        public MateralEnumHelperException(string message) : base(message)
        {
        }

        public MateralEnumHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MateralEnumHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
