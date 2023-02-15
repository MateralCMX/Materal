using Ocelot.Middleware;
using System.Net;
using System.Net.Http.Headers;

namespace Materal.Gateway.OcelotExtension.Middleware
{
    public class GatewayDownstreamResponse : DownstreamResponse
    {
        public GatewayDownstreamResponse(HttpContent content, HttpStatusCode statusCode, List<Header> headers, string reasonPhrase) : base(content, statusCode, headers, reasonPhrase)
        {
        }
        public GatewayDownstreamResponse(HttpResponseMessage response) : this(response.Content, response.StatusCode, response.Headers.Select(x => new Header(x.Key, x.Value)).ToList(), response.ReasonPhrase ?? string.Empty)
        {
            TrailingHeaders = response.TrailingHeaders;
        }
        public HttpResponseHeaders? TrailingHeaders { get; }
    }
}
