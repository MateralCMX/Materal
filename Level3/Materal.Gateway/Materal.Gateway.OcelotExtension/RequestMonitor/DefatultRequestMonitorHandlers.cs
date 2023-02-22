using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.Custom;
using Materal.Gateway.OcelotExtension.RequestMonitor.Model;

namespace Materal.Gateway.OcelotExtension.RequestMonitor
{
    public class DefatultRequestMonitorHandlers : IRequestMonitorHandlers
    {
        public readonly static List<Type> _handlerTypes = new();
        public void Add<T>() where T : class, IRequestMonitorHandler => AddHandler<T>();
        public static void AddHandler<T>() where T : class, IRequestMonitorHandler => _handlerTypes.Add(typeof(T));
        public static void AddHandler(Type handlerType)
        {
            if (handlerType.IsAssignableTo(typeof(IRequestMonitorHandler))) throw new GatewayException($"处理器必须实现{nameof(ICustomHandler)}");
            _handlerTypes.Add(handlerType);
        }
        public async Task HandlerRequstAsync(HandlerRequestModel model)
        {
            foreach (Type handlerType in _handlerTypes)
            {
                IRequestMonitorHandler handler = OcelotService.GetService<IRequestMonitorHandler>(handlerType);
                await handler.HandlerRequstAsync(model);
            }
        }
        public async Task HandlerResponseAsync(HandlerResponseModel model)
        {
            foreach (Type handlerType in _handlerTypes)
            {
                IRequestMonitorHandler handler = OcelotService.GetService<IRequestMonitorHandler>(handlerType);
                await handler.HandlerResponseAsync(model);
            }
        }
    }
}
