using Materal.Common;

namespace Materal.LoggerClient
{
    public class LoggerClientException : MateralException
    {
        public LoggerClientException()
        {
        }

        public LoggerClientException(string message) : base(message)
        {
        }

        public LoggerClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
