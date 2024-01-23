using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    /// <summary>
    /// 自定义处理器
    /// </summary>
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
        /// <returns></returns>
        Response<HttpResponseMessage?> AfterTransmit(HttpContext httpContext);
    }
}
