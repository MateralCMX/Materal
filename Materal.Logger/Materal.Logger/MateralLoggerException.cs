using System.Runtime.Serialization;

namespace Materal.Logger
{
    public class MateralLoggerException : Exception
    {
        public MateralLoggerException()
        {
        }

        public MateralLoggerException(string? message) : base(message)
        {
        }

        public MateralLoggerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MateralLoggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
