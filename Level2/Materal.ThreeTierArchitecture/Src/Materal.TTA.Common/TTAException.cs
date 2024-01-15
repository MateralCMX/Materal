namespace Materal.TTA.Common
{
    /// <summary>
    /// TTA异常
    /// </summary>
    public class TTAException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public TTAException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public TTAException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TTAException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
