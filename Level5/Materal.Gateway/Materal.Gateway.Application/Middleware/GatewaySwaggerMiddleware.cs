using Microsoft.AspNetCore.Http;
using MMLib.SwaggerForOcelot.Configuration;
using MMLib.SwaggerForOcelot.Middleware;
using MMLib.SwaggerForOcelot.Repositories;
using MMLib.SwaggerForOcelot.Transformation;
using Swashbuckle.AspNetCore.Swagger;

namespace Materal.Gateway.Application.Middleware
{
    /// <summary>
    /// 网关Swagger中间件
    /// </summary>
    public class GatewaySwaggerMiddleware(RequestDelegate next, SwaggerForOcelotUIOptions options, IOptionsMonitor<List<RouteOptions>> routes, ISwaggerJsonTransformer transformer, ISwaggerProvider swaggerProvider, ISwaggerDownstreamInterceptor? downstreamInterceptor = null)
    {
        private readonly SwaggerForOcelotMiddleware swaggerForOcelotMiddleware =new(next, options, routes,transformer, swaggerProvider, downstreamInterceptor);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="swaggerEndPointRepository"></param>
        /// <param name="downstreamSwaggerDocs"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ISwaggerEndPointProvider swaggerEndPointRepository, IDownstreamSwaggerDocsRepository downstreamSwaggerDocs)
        {
            try
            {
                await swaggerForOcelotMiddleware.Invoke(context, swaggerEndPointRepository, downstreamSwaggerDocs);
            }
            catch (Exception)
            {
                await next(context);
            }
        }
    }
}
