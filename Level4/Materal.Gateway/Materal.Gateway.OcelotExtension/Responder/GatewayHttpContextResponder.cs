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
    /// <summary>
    /// 网关HTTP上下文响应器
    /// </summary>
    public class GatewayHttpContextResponder : IHttpResponder
    {
        private readonly IRemoveOutputHeaders _removeOutputHeaders;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="removeOutputHeaders"></param>
        public GatewayHttpContextResponder(IRemoveOutputHeaders removeOutputHeaders)
        {
            _removeOutputHeaders = removeOutputHeaders;
        }
        /// <summary>
        /// 设置响应
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task SetResponseOnHttpContext(HttpContext context, DownstreamResponse? response)
        {
            if (response is null) return;
            _removeOutputHeaders.Remove(response.Headers);
            foreach (var httpResponseHeader in response.Headers)
            {
                AddHeaderIfDoesntExist(context, httpResponseHeader);
            }
            SetStatusCode(context, (int)response.StatusCode);
            IHttpResponseFeature? httpResponseFeature = context.Response.HttpContext.Features.Get<IHttpResponseFeature>();
            if (httpResponseFeature != null)
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
            if (response is GatewayDownstreamResponse gatewayResponse)
            {
                if (gatewayResponse.TrailingHeaders != null)
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
        /// <summary>
        /// 设置错误响应
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        public void SetErrorResponseOnContext(HttpContext context, int statusCode) => SetStatusCode(context, statusCode);
        /// <summary>
        /// 设置错误响应
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        private static void SetStatusCode(HttpContext context, int statusCode)
        {
            if (context.Response.HasStarted) return;
            context.Response.StatusCode = statusCode;
        }
        /// <summary>
        /// 添加头信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpResponseHeader"></param>
        private static void AddHeaderIfDoesntExist(HttpContext context, Header httpResponseHeader)
        {
            if (context.Response.Headers.ContainsKey(httpResponseHeader.Key)) return;
            context.Response.Headers.Add(httpResponseHeader.Key, new StringValues(httpResponseHeader.Values.ToArray()));
        }
    }
}
