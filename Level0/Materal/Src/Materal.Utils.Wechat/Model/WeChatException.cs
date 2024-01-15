namespace Materal.Utils.Wechat.Model
{
    /// <summary>
    /// 微信异常
    /// </summary>
    public class WechatException : MateralException
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public WechatException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public WechatException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WechatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
