using Microsoft.AspNetCore.Http;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    public interface ICustomHandlers
    {
        /// <summary>
        /// 添加
        /// </summary>
        void Add<T>() where T : class, ICustomHandler;
        /// <summary>
        /// 转发之前
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<(Response<HttpResponseMessage?> response, string handlerName)> BeforeTransmitAsync(HttpContext httpContext);
        /// <summary>
        /// 转发之后
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task<(Response<HttpResponseMessage?> response, string handlerName)> AfterTransmitAsync(HttpContext httpContext);
    }
}
