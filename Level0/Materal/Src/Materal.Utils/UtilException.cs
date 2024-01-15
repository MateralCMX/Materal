namespace Materal.Utils
{
    /// <summary>
    /// 工具异常
    /// </summary>
    public class UtilException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UtilException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public UtilException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UtilException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
