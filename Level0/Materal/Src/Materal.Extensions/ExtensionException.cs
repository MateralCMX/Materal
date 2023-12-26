namespace Materal.Extensions
{
    /// <summary>
    /// 扩展异常
    /// </summary>
    public class ExtensionException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ExtensionException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public ExtensionException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ExtensionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
