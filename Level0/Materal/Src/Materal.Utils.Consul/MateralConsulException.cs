namespace Materal.Utils.Consul
{
    /// <summary>
    /// Consul异常
    /// </summary>
    public class MateralConsulException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralConsulException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MateralConsulException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralConsulException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
