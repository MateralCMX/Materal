using Materal.Gateway.OcelotExtension.Middleware;
using Microsoft.AspNetCore.Http;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Requester.Middleware
{
    public class GatewayHttpRequesterMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpRequester _requester;

        public GatewayHttpRequesterMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IHttpRequester requester) : base(loggerFactory.CreateLogger<GatewayHttpRequesterMiddleware>())
        {
            _next = next;
            _requester = requester;
        }
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
