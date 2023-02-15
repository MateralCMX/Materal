using Materal.Gateway.OcelotExtension.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Ocelot.Headers;
using Ocelot.Middleware;
using Ocelot.Responder;
using System.Net;

namespace Materal.Gateway.OcelotExtension.Responder
{
    public class GatewayHttpContextResponder : IHttpResponder
    {
        private readonly IRemoveOutputHeaders _removeOutputHeaders;
        public GatewayHttpContextResponder(IRemoveOutputHeaders removeOutputHeaders)
        {
            _removeOutputHeaders = removeOutputHeaders;
        }
        public async Task SetResponseOnHttpContext(HttpContext context, DownstreamResponse response)
        {
            _removeOutputHeaders.Remove(response.Headers);
            foreach (var httpResponseHeader in response.Headers)
            {
                AddHeaderIfDoesntExist(context, httpResponseHeader);
            }
            SetStatusCode(context, (int)response.StatusCode);
            IHttpResponseFeature? httpResponseFeature = context.Response.HttpContext.Features.Get<IHttpResponseFeature>();
            if(httpResponseFeature != null)
            {
                httpResponseFeature.ReasonPhrase = response.ReasonPhrase;
            }
            if (response.Content is null)
            {
                return;
            }
            foreach (var httpResponseHeader in response.Content.Headers)
            {
                AddHeaderIfDoesntExist(context, new Header(httpResponseHeader.Key, httpResponseHeader.Value));
            }
            var content = await response.Content.ReadAsStreamAsync();
            if (response.Content.Headers.ContentLength != null)
            {
                AddHeaderIfDoesntExist(context, new Header("Content-Length", new[] { response.Content.Headers.ContentLength.ToString() }));
            }
            if(response is GatewayDownstreamResponse gatewayResponse)
            {
                if(gatewayResponse.TrailingHeaders != null)
                {
                    foreach (KeyValuePair<string, IEnumerable<string>> header in gatewayResponse.TrailingHeaders)
                    {
                        context.Response.AppendTrailer(header.Key, new StringValues(header.Value.ToArray()));
                    }
                }
            }
            using (content)
            {
                if (response.StatusCode != HttpStatusCode.NotModified && context.Response.ContentLength != 0)
                {
                    await content.CopyToAsync(context.Response.Body);
                }
            }
        }
        public void SetErrorResponseOnContext(HttpContext context, int statusCode) => SetStatusCode(context, statusCode);
        public async Task SetErrorResponseOnContext(HttpContext context, DownstreamResponse response)
        {
            Stream content = await response.Content.ReadAsStreamAsync();
            if (response.Content.Headers.ContentLength != null)
            {
                AddHeaderIfDoesntExist(context, new Header("Content-Length", new[] { response.Content.Headers.ContentLength.ToString() }));
            }
            using (content)
            {
                if (context.Response.ContentLength == 0) return;
                await content.CopyToAsync(context.Response.Body);
            }
        }
        private static void SetStatusCode(HttpContext context, int statusCode)
        {
            if (context.Response.HasStarted) return;
            context.Response.StatusCode = statusCode;
        }
        private static void AddHeaderIfDoesntExist(HttpContext context, Header httpResponseHeader)
        {
            if (context.Response.Headers.ContainsKey(httpResponseHeader.Key)) return;
            context.Response.Headers.Add(httpResponseHeader.Key, new StringValues(httpResponseHeader.Values.ToArray()));
        }
    }
}
