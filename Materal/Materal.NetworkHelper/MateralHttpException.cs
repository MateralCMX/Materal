using System;
using System.Net;

namespace Materal.NetworkHelper
{
    public class MateralHttpException : MateralNetworkException
    {
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        public MateralHttpException(HttpStatusCode httpStatusCode) : base()
        {
            StatusCode = httpStatusCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            StatusCode = httpStatusCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = httpStatusCode;
        }
    }
}
