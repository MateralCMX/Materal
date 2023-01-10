namespace Materal.BaseCore.Common
{
    /// <summary>
    /// 发布中心异常
    /// </summary>
    public class MateralCoreException : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralCoreException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MateralCoreException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralCoreException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
