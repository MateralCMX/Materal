namespace Materal.Gateway
{
    /// <summary>
    /// 网关异常
    /// </summary>
    public class GatewayException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public GatewayException() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public GatewayException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public GatewayException(string message, Exception innerException) : base(message, innerException) { }
    }
}
