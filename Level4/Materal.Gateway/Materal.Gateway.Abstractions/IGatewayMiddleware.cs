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
        /// <returns>true:处理完毕返回httpContext,false:处理未完成，继续执行管道</returns>
        Task<bool> InvokeAsync(HttpContext httpContext);
    }
}
