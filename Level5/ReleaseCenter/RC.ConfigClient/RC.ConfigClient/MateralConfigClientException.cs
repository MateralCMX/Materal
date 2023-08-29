using Materal.Abstractions;

namespace ConfigCenter.Client
{
    /// <summary>
    /// 配置客户端异常
    /// </summary>
    public class MateralConfigClientException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralConfigClientException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MateralConfigClientException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralConfigClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
