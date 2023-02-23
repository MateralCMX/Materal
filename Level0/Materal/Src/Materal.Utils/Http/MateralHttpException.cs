using Materal.Abstractions;

namespace Materal.Utils.Http
{
    /// <summary>
    /// Http异常
    /// </summary>
    public class MateralHttpException : MateralException
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public HttpRequestMessage? HttpRequestMessage { get; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public HttpResponseMessage? HttpResponseMessage { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage):this(httpRequestMessage, httpResponseMessage, $"Http请求错误{Convert.ToInt32(httpResponseMessage.StatusCode)}")
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, string message) : base(message)
        {
            HttpRequestMessage = httpRequestMessage;
            HttpResponseMessage = httpResponseMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, string message, Exception innerException) : base(message, innerException)
        {
            HttpRequestMessage = httpRequestMessage;
            HttpResponseMessage = httpResponseMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, string message) : base(message)
        {
            HttpRequestMessage = httpRequestMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, string message, Exception innerException) : base(message, innerException)
        {
            HttpRequestMessage = httpRequestMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MateralHttpException(string message) : base(message)
        {
        }
    }
}
