namespace MMB.Core.Abstractions
{
    /// <summary>
    /// MMB异常
    /// </summary>
    public class MMBException : MergeBlockException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MMBException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MMBException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MMBException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
