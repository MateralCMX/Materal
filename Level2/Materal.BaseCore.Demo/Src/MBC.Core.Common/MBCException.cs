using Materal.BaseCore.Common;

namespace MBC.Core.Common
{
    public class MBCException : MateralCoreException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MBCException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MBCException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MBCException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
