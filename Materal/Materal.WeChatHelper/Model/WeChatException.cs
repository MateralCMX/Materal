using Materal.Common;

namespace Materal.WeChatHelper.Model
{
    public class WeChatException : MateralException
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        public WeChatException(string message) : base(message)
        {
        }
        public WeChatException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
