namespace MMB.Core.Abstractions
{
    /// <summary>
    /// MMB异常
    /// </summary>
    public class MMBException : MergeBlockModuleException
    {
        /// <summary>
        /// MMB异常
        /// </summary>
        public MMBException()
        {
        }
        /// <summary>
        /// MMB异常
        /// </summary>
        /// <param name="message"></param>
        public MMBException(string? message) : base(message)
        {
        }
        /// <summary>
        /// MMB异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MMBException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
