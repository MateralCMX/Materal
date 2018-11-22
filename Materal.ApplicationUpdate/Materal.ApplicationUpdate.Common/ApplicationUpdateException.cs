using Materal.Common;
using System;
using System.Runtime.Serialization;

namespace Materal.ApplicationUpdate.Common
{
    public class ApplicationUpdateException : MateralException
    {
        public ApplicationUpdateException()
        {
        }

        public ApplicationUpdateException(string message) : base(message)
        {
        }

        public ApplicationUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
