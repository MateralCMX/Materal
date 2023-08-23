using Materal.Gateway.Common;
using Microsoft.AspNetCore.Http;

namespace Materal.Gateway.OcelotExtension.Custom
{
    /// <summary>
    /// 网关中间件总线
    /// </summary>
    public interface IGatewayMiddlewareBus
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="type"></param>
        void Subscribe(Type type);
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="type"></param>
        void Unsubscribe(Type type);
        /// <summary>
        /// 订阅
        /// </summary>
        void Subscribe<T>() where T : IGatewayMiddleware;
        /// <summary>
        /// 取消订阅
        /// </summary>
        void Unsubscribe<T>() where T : IGatewayMiddleware;
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<bool> InvokAsync(IServiceProvider serviceProvider, HttpContext httpContext);
    }
}
