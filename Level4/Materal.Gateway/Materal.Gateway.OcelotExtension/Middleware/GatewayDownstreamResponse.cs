using Ocelot.Middleware;
using System.Net;
using System.Net.Http.Headers;

namespace Materal.Gateway.OcelotExtension.Middleware
{
    /// <summary>
    /// ����������Ӧ
    /// </summary>
    public class GatewayDownstreamResponse : DownstreamResponse
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="content"></param>
        /// <param name="statusCode"></param>
        /// <param name="headers"></param>
        /// <param name="reasonPhrase"></param>
        public GatewayDownstreamResponse(HttpContent content, HttpStatusCode statusCode, List<Header> headers, string reasonPhrase) : base(content, statusCode, headers, reasonPhrase)
        {
        }
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="response"></param>
        public GatewayDownstreamResponse(HttpResponseMessage response) : this(response.Content, response.StatusCode, response.Headers.Select(x => new Header(x.Key, x.Value)).ToList(), response.ReasonPhrase ?? string.Empty)
        {
            TrailingHeaders = response.TrailingHeaders;
        }
        /// <summary>
        /// β����Ӧͷ
        /// </summary>
        public HttpResponseHeaders? TrailingHeaders { get; }
    }
}
