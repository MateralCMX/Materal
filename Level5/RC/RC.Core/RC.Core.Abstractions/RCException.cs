namespace RC.Core.Abstractions
{
    /// <summary>
    /// RC异常
    /// </summary>
    public class RCException : MergeBlockModuleException
    {
        /// <summary>
        /// RC异常
        /// </summary>
        public RCException()
        {
        }
        /// <summary>
        /// RC异常
        /// </summary>
        /// <param name="message"></param>
        public RCException(string? message) : base(message)
        {
        }
        /// <summary>
        /// RC异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RCException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}