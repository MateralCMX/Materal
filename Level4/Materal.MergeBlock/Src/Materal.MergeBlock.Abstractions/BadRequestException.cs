namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 坏请求异常
    /// </summary>
    public class BadRequestException : HttpCodeException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public BadRequestException() : base(400)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public BadRequestException(string? message) : base(400, message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BadRequestException(string? message, Exception? innerException) : base(400, message, innerException)
        {
        }
    }
}
