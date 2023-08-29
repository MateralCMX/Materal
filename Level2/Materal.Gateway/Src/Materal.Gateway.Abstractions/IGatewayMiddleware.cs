using Microsoft.AspNetCore.Http;

namespace Materal.Gateway.Abstractions
{
    /// <summary>
    /// 网关中间件
    /// </summary>
    public interface IGatewayMiddleware
    {
        /// <summary>
        /// 位序
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="httpContext"></param>
        Task InvokeAsync(HttpContext httpContext);
    }
}
