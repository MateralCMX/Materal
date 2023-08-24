using Materal.Gateway.OcelotExtension.Middleware;
using Microsoft.AspNetCore.Http;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Requester.Middleware
{
    /// <summary>
    /// 网关Http请求中间件
    /// </summary>
    public class GatewayHttpRequesterMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpRequester _requester;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="requester"></param>
        public GatewayHttpRequesterMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IHttpRequester requester) : base(loggerFactory.CreateLogger<GatewayHttpRequesterMiddleware>())
        {
            _next = next;
            _requester = requester;
        }
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var downstreamRoute = httpContext.Items.DownstreamRoute();
            Response<HttpResponseMessage> response = await _requester.GetResponse(httpContext);
            if (response.IsError)
            {
                httpContext.Items.UpsertErrors(response.Errors);
                return;
            }
            httpContext.Items.UpsertDownstreamResponse(new GatewayDownstreamResponse(response.Data));
            await _next.Invoke(httpContext);
        }
    }
}
