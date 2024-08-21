namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB工具异常
    /// </summary>
    public class MongoUtilException : UtilException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MongoUtilException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>

        public MongoUtilException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MongoUtilException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
