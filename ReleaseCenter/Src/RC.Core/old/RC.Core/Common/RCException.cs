using System.Runtime.Serialization;

namespace RC.Core.Common
{
    /// <summary>
    /// 发布中心异常
    /// </summary>
    public class RCException : Exception
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
