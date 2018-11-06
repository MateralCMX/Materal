using Materal.Common;
using System;
using System.Runtime.Serialization;

namespace Materal.DBHelper
{
    public class MateralDBHelperException : MateralException
    {
        public MateralDBHelperException()
        {
        }

        public MateralDBHelperException(string message) : base(message)
        {
        }

        public MateralDBHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MateralDBHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
