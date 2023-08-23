using CacheManager.Core.Logging;
using Materal.Gateway.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Materal.Gateway.OcelotExtension.Custom
{
    /// <summary>
    /// 网关中间件总线
    /// </summary>
    public class GatewayMiddlewareBusImpl : IGatewayMiddlewareBus
    {
        private readonly List<Type> _types = new();
        private readonly ILogger<GatewayMiddlewareBusImpl> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        public GatewayMiddlewareBusImpl(ILogger<GatewayMiddlewareBusImpl> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Subscribe<T>() where T : IGatewayMiddleware => Subscribe(typeof(T));
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Unsubscribe<T>() where T : IGatewayMiddleware => Unsubscribe(typeof(T));
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Subscribe(Type type)
        {
            if (_types.Contains(type)) return;
            if (!type.IsAssignableTo<IGatewayMiddleware>()) return;
            _types.Add(type);
            _logger.LogDebug($"网关中间件注册:{type.FullName}");
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="type"></param>
        public void Unsubscribe(Type type)
        {
            if (!_types.Contains(type)) return;
            _types.Remove(type);
            _logger.LogDebug($"网关中间件注销:{type.FullName}");
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<bool> InvokAsync(IServiceProvider serviceProvider, HttpContext httpContext)
        {
            foreach (Type type in _types)
            {
                IGatewayMiddleware? gatewayMiddleware = type.Instantiation<IGatewayMiddleware>(serviceProvider);
                if (gatewayMiddleware is null) continue;
                _logger.LogDebug($"网关中间件[{type.FullName}]执行");
                bool result = await gatewayMiddleware.InvokeAsync(httpContext);
                if (result)
                {
                    _logger.LogDebug($"网关中间件[{type.FullName}]执行结果:拦截请求");
                    return false;
                }
                else
                {
                    _logger.LogDebug($"网关中间件[{type.FullName}]执行结果:请求继续");
                }
            }
            return true;
        }
    }
}
