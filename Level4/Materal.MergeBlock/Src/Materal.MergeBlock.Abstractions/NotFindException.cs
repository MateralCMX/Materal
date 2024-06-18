namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 未找到异常
    /// </summary>
    public class NotFindException : HttpCodeException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public NotFindException() : base(404)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public NotFindException(string? message) : base(404, message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotFindException(string? message, Exception? innerException) : base(404, message, innerException)
        {
        }
    }
}
