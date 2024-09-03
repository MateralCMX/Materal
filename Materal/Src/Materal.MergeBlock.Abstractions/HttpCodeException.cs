namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// HTTP状态码异常
    /// </summary>
    public class HttpCodeException : Exception
    {
        /// <summary>
        /// HTTP状态码
        /// </summary>
        public int HttpCode { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpCode"></param>
        public HttpCodeException(int httpCode)
        {
            HttpCode = httpCode;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpCode"></param>
        /// <param name="message"></param>
        public HttpCodeException(int httpCode, string? message) : base(message)
        {
            HttpCode = httpCode;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public HttpCodeException(int httpCode, string? message, Exception? innerException) : base(message, innerException)
        {
            HttpCode = httpCode;
        }
    }
}
