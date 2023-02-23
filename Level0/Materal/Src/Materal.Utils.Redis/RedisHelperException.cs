namespace Materal.Utils.Redis
{
    /// <inheritdoc />
    /// <summary>
    /// Redis工具异常类
    /// </summary>
    public class RedisUtilException : UtilException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public RedisUtilException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public RedisUtilException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RedisUtilException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
