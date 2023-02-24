using Materal.Abstractions;

namespace ConfigCenter.Client
{
    public class MateralConfigClientException : MateralException
    {
        public MateralConfigClientException()
        {
        }
        public MateralConfigClientException(string message) : base(message)
        {
        }
        public MateralConfigClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
