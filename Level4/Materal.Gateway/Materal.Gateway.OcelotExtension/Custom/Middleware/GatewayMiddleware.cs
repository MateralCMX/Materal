using Microsoft.AspNetCore.Http;
using Ocelot.Logging;
using Ocelot.Middleware;

namespace Materal.Gateway.OcelotExtension.Custom.Middleware
{
    /// <summary>
    /// 网关中间件
    /// </summary>
    public class GatewayMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGatewayMiddlewareBus _gatewayMiddlewareBus;
        /// <summary>
        /// 构造方法
        /// </summary>
        public GatewayMiddleware(IServiceProvider serviceProvider, IOcelotLoggerFactory loggerFactory, RequestDelegate next, IGatewayMiddlewareBus gatewayMiddlewareBus) : base(loggerFactory.CreateLogger<GatewayCustomMiddleware>())
        {
            _serviceProvider = serviceProvider;
            _next = next;
            _gatewayMiddlewareBus = gatewayMiddlewareBus;
        }
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            bool canNext = await _gatewayMiddlewareBus.InvokAsync(_serviceProvider, httpContext);
            if (canNext) await _next.Invoke(httpContext);
        }
    }
}
