namespace MateralPublish
{
    public class MateralPublishException : Exception
    {
        public MateralPublishException()
        {
        }

        public MateralPublishException(string? message) : base(message)
        {
        }

        public MateralPublishException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
