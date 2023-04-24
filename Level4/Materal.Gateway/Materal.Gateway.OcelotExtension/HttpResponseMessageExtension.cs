using System.Net;

namespace Materal.Gateway.OcelotExtension
{
    public static class HttpResponseMessageHelper
    {
        /// <summary>
        /// 获得重定向302响应消息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetRedirectHttpResponseMessage(string url)
        {
            HttpResponseMessage result = new()
            {
                StatusCode = HttpStatusCode.Redirect
            };
            result.Headers.Add("Location", url);
            return result;
        }
    }
}
