namespace MMC.Core.Abstractions
{
    /// <summary>
    /// MMC异常
    /// </summary>
    public class MMCException : MergeBlockException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MMCException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MMCException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MMCException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
