using System;
using System.Collections.Generic;
using System.Net;
using Materal.ConvertHelper;

namespace Materal.NetworkHelper
{
    public class MateralHttpException : MateralNetworkException
    {
        /// <summary>
        /// 错误状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; }
        /// <summary>
        /// Http头
        /// </summary>
        public Dictionary<string, string> Heads { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="heads"></param>
        public MateralHttpException(string url, HttpStatusCode httpStatusCode, Dictionary<string, string> heads = null)
        {
            Url = url;
            StatusCode = httpStatusCode;
            Heads = heads;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <param name="heads"></param>
        public MateralHttpException(string url, HttpStatusCode httpStatusCode, string message, Dictionary<string, string> heads = null) : base(message)
        {
            Url = url;
            StatusCode = httpStatusCode;
            Heads = heads;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="heads"></param>
        public MateralHttpException(string url, HttpStatusCode httpStatusCode, string message, Exception innerException, Dictionary<string, string> heads = null) : base(message, innerException)
        {
            Url = url;
            StatusCode = httpStatusCode;
            Heads = heads;
        }
        /// <summary>
        /// 获得消息
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            string message = $"Http请求错误:\r\nStatusCode:{Convert.ToInt32(StatusCode)}({StatusCode.ToString()})\r\nUrl:{Url}\r\n";
            if (Heads != null)
            {
                message += $"Heads:{Heads.ToJson()}\r\n";
            }
            message += $"message:{Message}\r\nStackTrace:{StackTrace}";
            return message;
        }
    }
}
