using Materal.Gateway.OcelotExtension.Middleware;
using Ocelot.Errors;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom.Middleware
{
    /// <summary>
    /// 网关自定义中间件
    /// </summary>
    public class GatewayCustomMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICustomHandlers _customHandlers;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="next"></param>
        /// <param name="customHandlers"></param>
        public GatewayCustomMiddleware(IOcelotLoggerFactory loggerFactory, RequestDelegate next, ICustomHandlers customHandlers) : base(loggerFactory.CreateLogger<GatewayCustomMiddleware>())
        {
            _next = next;
            _customHandlers = customHandlers;
        }
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            (Response<HttpResponseMessage?> result, string handlerName) = await _customHandlers.BeforeTransmitAsync(httpContext);
            if (!UpdateDownstreamResponse(httpContext, result, handlerName)) return;
            await _next.Invoke(httpContext);
            (result, handlerName) = await _customHandlers.AfterTransmitAsync(httpContext);
            if (!UpdateDownstreamResponse(httpContext, result, handlerName)) return;
        }
        /// <summary>
        /// 更新下游返回
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="response"></param>
        /// <param name="reasonPhrase"></param>
        /// <returns>是否继续执行</returns>
        private static bool UpdateDownstreamResponse(HttpContext httpContext, Response<HttpResponseMessage?> response, string reasonPhrase)
        {
            if (response.Data != null)
            {
                httpContext.Items.UpsertDownstreamResponse(new GatewayDownstreamResponse(response.Data));
                return false;
            }
            else if (response.IsError)
            {
                HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
                string message = "自定义处理失败";
                foreach (Error error in response.Errors)
                {
                    try { httpStatusCode = (HttpStatusCode)error.HttpStatusCode; }
                    catch { }
                    message = error.Message;
                }
                List<Header> headers = new()
                {
                    new Header("Server", new string[] { "Materal.Gateway" })
                };
                HttpContent content = new StringContent(message, Encoding.UTF8, "text/plain");
                DownstreamResponse downstreamResponse = new(content, httpStatusCode, headers, reasonPhrase);
                httpContext.Items.UpsertDownstreamResponse(downstreamResponse);
                return false;
            }
            return true;
        }
    }
}
