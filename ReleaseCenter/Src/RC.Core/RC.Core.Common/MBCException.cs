using Materal.BaseCore.Common;

namespace RC.Core.Common
{
    public class RCException : MateralCoreException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public RCException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public RCException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RCException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
