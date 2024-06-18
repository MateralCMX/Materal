namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 未授权异常
    /// </summary>
    public class UnauthorizedException : HttpCodeException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UnauthorizedException() : base(401)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public UnauthorizedException(string? message) : base(401, message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UnauthorizedException(string? message, Exception? innerException) : base(401, message, innerException)
        {
        }
    }

}
