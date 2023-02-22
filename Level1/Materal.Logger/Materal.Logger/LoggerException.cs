using Materal.Common;

namespace Materal.Logger
{
    /// <summary>
    /// 日志异常
    /// </summary>
    public class LoggerException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public LoggerException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public LoggerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
