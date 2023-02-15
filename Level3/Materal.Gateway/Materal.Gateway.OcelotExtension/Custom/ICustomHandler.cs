using Microsoft.AspNetCore.Http;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    public interface ICustomHandler
    {
        /// <summary>
        /// 转发之前
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<Response<HttpResponseMessage?>> BeforeTransmitAsync(HttpContext httpContext);
        /// <summary>
        /// 转发之后
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task<Response<HttpResponseMessage?>> AfterTransmitAsync(HttpContext httpContext);
        /// <summary>
        /// 转发之前
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Response<HttpResponseMessage?> BeforeTransmit(HttpContext httpContext);
        /// <summary>
        /// 转发之后
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Response<HttpResponseMessage?> AfterTransmit(HttpContext httpContext);
    }
}
