using Materal.Common;
using System;
using System.Runtime.Serialization;

namespace Materal.StringHelper
{
    public class MateralStringHelperException : MateralException
    {
        public MateralStringHelperException()
        {
        }

        public MateralStringHelperException(string message) : base(message)
        {
        }

        public MateralStringHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MateralStringHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
