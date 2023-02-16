using Materal.Gateway.Common;
using Microsoft.AspNetCore.Http;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    public class DefaultCustomHandlers : ICustomHandlers
    {
        public readonly static List<Type> _handlerTypes = new();
        public void Add<T>() where T : class, ICustomHandler => AddHandler<T>();
        public static void AddHandler<T>() where T : class, ICustomHandler => _handlerTypes.Add(typeof(T));
        public static void AddHandler(Type handlerType)
        {
            if (handlerType.IsAssignableTo(typeof(ICustomHandler))) throw new MateralGatewayException($"处理器必须实现{nameof(ICustomHandler)}");
            _handlerTypes.Add(handlerType);
        }
        public async Task<(Response<HttpResponseMessage?> response, string handlerName)> AfterTransmitAsync(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            foreach (Type handlerType in _handlerTypes)
            {
                ICustomHandler handler = OcelotService.GetService<ICustomHandler>(handlerType);
                result = await handler.AfterTransmitAsync(httpContext);
                if (result.Data != null) return (result, handlerType.Name);
                if (result.IsError) return (result, handlerType.Name);
            }
            return (result, nameof(DefaultCustomHandlers));
        }
        public async Task<(Response<HttpResponseMessage?> response, string handlerName)> BeforeTransmitAsync(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            foreach (Type handlerType in _handlerTypes)
            {
                ICustomHandler handler = OcelotService.GetService<ICustomHandler>(handlerType);
                result = await handler.BeforeTransmitAsync(httpContext);
                if (result.Data != null) return (result, handlerType.Name);
                if (result.IsError) return (result, handlerType.Name);
            }
            return (result, nameof(DefaultCustomHandlers));
        }
    }
}
