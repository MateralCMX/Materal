using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;

namespace Materal.DotNetty.Common
{
    public static class HttpResponseHelper
    {
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status">状态码</param>
        /// <returns></returns>
        public static IFullHttpResponse GetHttpResponse(HttpResponseStatus status)
        {
            Dictionary<AsciiString, object> headers = GetDefaultHeaders();
            return GetHttpResponse(status, headers);
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status">状态码</param>
        /// <param name="body">Body</param>
        /// <returns></returns>
        public static IFullHttpResponse GetHttpResponse(HttpResponseStatus status, string body)
        {
            Dictionary<AsciiString, object> headers = GetDefaultHeaders("text/plain;charset-UTF-8");
            return GetHttpResponse(status, body, headers);
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status">状态码</param>
        /// <param name="body">Body</param>
        /// <returns></returns>
        public static IFullHttpResponse GetHttpResponse(HttpResponseStatus status, byte[] body)
        {
            Dictionary<AsciiString, object> headers = GetDefaultHeaders("application/octet-stream");
            return GetHttpResponse(status, body, headers);
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status">状态码</param>
        /// <param name="headers">Header</param>
        /// <returns></returns>
        public static IFullHttpResponse GetHttpResponse(HttpResponseStatus status, Dictionary<AsciiString, object> headers)
        {
            return GetHttpResponse(status, string.Empty, headers);
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status">状态码</param>
        /// <param name="body">Body</param>
        /// <param name="headers">Header</param>
        /// <returns></returns>
        public static IFullHttpResponse GetHttpResponse(HttpResponseStatus status, string body, Dictionary<AsciiString, object> headers)
        {
            byte[] bodyData = string.IsNullOrEmpty(body) ? new byte[0] : Encoding.UTF8.GetBytes(body);
            return GetHttpResponse(status, bodyData, headers);
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status">状态码</param>
        /// <param name="body">Body</param>
        /// <param name="headers">Header</param>
        /// <returns></returns>
        public static IFullHttpResponse GetHttpResponse(HttpResponseStatus status, byte[] body, Dictionary<AsciiString, object> headers)
        {
            DefaultFullHttpResponse result;
            if (body != null && body.Length > 0)
            {
                IByteBuffer bodyBuffer = Unpooled.WrappedBuffer(body);
                result = new DefaultFullHttpResponse(HttpVersion.Http11, status, bodyBuffer);
            }
            else
            {
                result = new DefaultFullHttpResponse(HttpVersion.Http11, status);
            }
            foreach ((AsciiString key, object value) in headers)
            {
                result.Headers.Set(key, value);
            }
            return result;
        }
        /// <summary>
        /// 获得默认的HttpHeaders
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static Dictionary<AsciiString, object> GetDefaultHeaders(string contentType = null)
        {
            var result = new Dictionary<AsciiString, object>
            {
                { HttpHeaderNames.Date, DateTime.Now },
                { HttpHeaderNames.Server,"MateralDotNettyServer" },
                { HttpHeaderNames.AcceptLanguage,"zh-CN,zh;q=0.9" },
                { HttpHeaderNames.AccessControlAllowOrigin,"*" },
            };
            if (!string.IsNullOrEmpty(contentType))
            {
                result.Add(HttpHeaderNames.ContentType, contentType);
            }
            return result;
        }
    }
}
