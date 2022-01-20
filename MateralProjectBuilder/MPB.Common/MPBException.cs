using System;
using System.Runtime.Serialization;

namespace MPB.Common
{
    public class MPBException : Exception
    {
        public MPBException()
        {
        }

        public MPBException(string message) : base(message)
        {
        }

        public MPBException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MPBException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
