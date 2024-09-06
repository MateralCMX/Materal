using Ocelot.Errors;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responder;

namespace Materal.Gateway.OcelotExtension.Responder.Middleware
{
    /// <summary>
    /// 网关响应中间件
    /// </summary>
    public class GatewayResponderMiddleware(RequestDelegate next, IHttpResponder responder, IOcelotLoggerFactory loggerFactory, IErrorsToHttpStatusCodeMapper codeMapper) : OcelotMiddleware(loggerFactory.CreateLogger<GatewayResponderMiddleware>())
    {
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            await next(httpContext);
            List<Error> errors = httpContext.Items.Errors();
            if (errors.Count > 0)
            {
                SetErrorResponse(httpContext, errors);
            }
            else
            {
                DownstreamResponse downstreamResponse = httpContext.Items.DownstreamResponse();
                await responder.SetResponseOnHttpContext(httpContext, downstreamResponse);
            }
        }
        /// <summary>
        /// 设置错误响应
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errors"></param>
        private void SetErrorResponse(HttpContext context, List<Error> errors)
        {
            int statusCode = codeMapper.Map(errors);
            responder.SetErrorResponseOnContext(context, statusCode);
            if (errors.Any(e => e.Code == OcelotErrorCode.QuotaExceededError))
            {
                DownstreamResponse downstreamResponse = context.Items.DownstreamResponse();
                responder.SetErrorResponseOnContext(context, downstreamResponse);
            }
        }
    }
}
