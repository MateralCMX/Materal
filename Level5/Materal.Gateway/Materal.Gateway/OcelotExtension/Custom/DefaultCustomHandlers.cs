using Materal.Gateway.Common;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    /// <summary>
    /// 默认自定义处理器组
    /// </summary>
    public class DefaultCustomHandlers : ICustomHandlers
    {
        private readonly static List<Type> _handlerTypes = new();
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>() where T : class, ICustomHandler => AddHandler<T>();
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void AddHandler<T>() where T : class, ICustomHandler => _handlerTypes.Add(typeof(T));
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handlerType"></param>
        /// <exception cref="GatewayException"></exception>
        public static void AddHandler(Type handlerType)
        {
            if (handlerType.IsAssignableTo(typeof(ICustomHandler))) throw new GatewayException($"处理器必须实现{nameof(ICustomHandler)}");
            _handlerTypes.Add(handlerType);
        }
        /// <summary>
        /// 转发之后
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<(Response<HttpResponseMessage?> response, string handlerName)> AfterTransmitAsync(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            foreach (Type handlerType in _handlerTypes)
            {
                ICustomHandler handler = OcelotService.GetService<ICustomHandler>(handlerType);
                result = await handler.AfterTransmitAsync(httpContext);
                if (result.Data is not null) return (result, handlerType.Name);
                if (result.IsError) return (result, handlerType.Name);
            }
            return (result, nameof(DefaultCustomHandlers));
        }
        /// <summary>
        /// 转发之前
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<(Response<HttpResponseMessage?> response, string handlerName)> BeforeTransmitAsync(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            foreach (Type handlerType in _handlerTypes)
            {
                ICustomHandler handler = OcelotService.GetService<ICustomHandler>(handlerType);
                result = await handler.BeforeTransmitAsync(httpContext);
                if (result.Data is not null) return (result, handlerType.Name);
                if (result.IsError) return (result, handlerType.Name);
            }
            return (result, nameof(DefaultCustomHandlers));
        }
    }
}
