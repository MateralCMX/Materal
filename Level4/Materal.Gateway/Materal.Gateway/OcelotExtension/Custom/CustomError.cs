using Ocelot.Errors;
using System.Net;

namespace Materal.Gateway.OcelotExtension.Custom
{
    /// <summary>
    /// 自定义错误
    /// </summary>
    public class CustomError : Error
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        public CustomError(string message, HttpStatusCode httpStatusCode) : base(message, OcelotErrorCode.UnknownError, (int)httpStatusCode)
        {
        }
    }
}
