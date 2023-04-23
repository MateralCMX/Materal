using Materal.Gateway.OcelotExtension.RequestMonitor.Model;
using Microsoft.AspNetCore.Http;
using Ocelot.Configuration;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;
using System.Threading.Tasks.Dataflow;

namespace Materal.Gateway.OcelotExtension.RequestMonitor.Middleware
{
    /// <summary>
    /// 请求监控中间件
    /// </summary>
    public class GatewayRequestMonitorMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ActionBlock<HandlerRequestModel> _requestActionBlock = new ActionBlock<HandlerRequestModel>(HandlerRequstAsync);
        private static readonly ActionBlock<HandlerResponseModel> _responseActionBlock = new ActionBlock<HandlerResponseModel>(HandlerResponseAsync);
        public GatewayRequestMonitorMiddleware(IOcelotLoggerFactory loggerFactory, RequestDelegate next) : base(loggerFactory.CreateLogger<GatewayRequestMonitorMiddleware>())
        {
            _next = next;
        }
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            Guid ID = Guid.NewGuid();
            DownstreamRoute? downstreamRoute = httpContext.Items.DownstreamRoute();
            if (downstreamRoute == null) return;
            DownstreamRequest? downstreamRequest = httpContext.Items.DownstreamRequest();
            if (downstreamRequest == null) return;
            _requestActionBlock.Post(new HandlerRequestModel(ID, downstreamRoute, downstreamRequest));
            await _next.Invoke(httpContext);
            DownstreamResponse? downstreamResponse = httpContext.Items.DownstreamResponse();
            if (downstreamResponse == null) return;
            _responseActionBlock.Post(new HandlerResponseModel(ID, downstreamRoute, downstreamRequest, downstreamResponse));
        }
        private static async Task HandlerRequstAsync(HandlerRequestModel model)
        {
            IRequestMonitorHandlers requestMonitorHandler = OcelotService.GetService<IRequestMonitorHandlers>();
            await requestMonitorHandler.HandlerRequstAsync(model);
        }
        private static async Task HandlerResponseAsync(HandlerResponseModel model)
        {
            IRequestMonitorHandlers requestMonitorHandler = OcelotService.GetService<IRequestMonitorHandlers>();
            await requestMonitorHandler.HandlerResponseAsync(model);
        }
    }
}
