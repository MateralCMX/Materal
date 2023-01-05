using System.Runtime.Serialization;

namespace RC.Core.Common
{
    public class RCException : Exception
    {
        public RCException()
        {
        }

        public RCException(string? message) : base(message)
        {
        }

        public RCException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RCException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
