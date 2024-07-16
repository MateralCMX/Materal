namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存异常
    /// </summary>
    public class ContextCacheException : Exception
    {
        /// <summary>
        /// 上下文缓存异常
        /// </summary>
        public ContextCacheException()
        {
        }
        /// <summary>
        /// 上下文缓存异常
        /// </summary>
        /// <param name="message"></param>
        public ContextCacheException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 上下文缓存异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ContextCacheException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
