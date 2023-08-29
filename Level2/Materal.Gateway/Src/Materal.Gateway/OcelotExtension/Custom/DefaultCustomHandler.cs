using Microsoft.AspNetCore.Http;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    /// <summary>
    /// 默认自定义处理器
    /// </summary>
    public class DefaultCustomHandler : ICustomHandler
    {
        /// <summary>
        /// 转发之前
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public virtual Response<HttpResponseMessage?> BeforeTransmit(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            return result;
        }
        /// <summary>
        /// 转发之前
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public virtual Task<Response<HttpResponseMessage?>> BeforeTransmitAsync(HttpContext httpContext) => Task.FromResult(BeforeTransmit(httpContext));
        /// <summary>
        /// 转发之后
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public virtual Response<HttpResponseMessage?> AfterTransmit(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            return result;
        }
        /// <summary>
        /// 转发之后
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public virtual Task<Response<HttpResponseMessage?>> AfterTransmitAsync(HttpContext httpContext) => Task.FromResult(AfterTransmit(httpContext));
    }
}
