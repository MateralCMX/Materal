using System.Runtime.Serialization;

namespace Materal.LoggerClient
{
    public class LoggerClientException : Exception
    {
        public LoggerClientException()
        {
        }

        public LoggerClientException(string? message) : base(message)
        {
        }

        public LoggerClientException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LoggerClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
