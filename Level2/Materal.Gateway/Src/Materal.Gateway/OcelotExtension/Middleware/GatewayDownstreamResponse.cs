using Ocelot.Middleware;
using System.Net;
using System.Net.Http.Headers;

namespace Materal.Gateway.OcelotExtension.Middleware
{
    /// <summary>
    /// 网关下游响应
    /// </summary>
    public class GatewayDownstreamResponse : DownstreamResponse
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="content"></param>
        /// <param name="statusCode"></param>
        /// <param name="headers"></param>
        /// <param name="reasonPhrase"></param>
        public GatewayDownstreamResponse(HttpContent content, HttpStatusCode statusCode, List<Header> headers, string reasonPhrase) : base(content, statusCode, headers, reasonPhrase)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="response"></param>
        public GatewayDownstreamResponse(HttpResponseMessage response) : this(response.Content, response.StatusCode, response.Headers.Select(x => new Header(x.Key, x.Value)).ToList(), response.ReasonPhrase ?? string.Empty)
        {
            TrailingHeaders = response.TrailingHeaders;
        }
        /// <summary>
        /// 尾部响应头
        /// </summary>
        public HttpResponseHeaders? TrailingHeaders { get; }
    }
}
