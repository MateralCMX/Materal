using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.Custom;
using Materal.Gateway.OcelotExtension.RequestMonitor.Model;

namespace Materal.Gateway.OcelotExtension.RequestMonitor
{
    /// <summary>
    /// 默认请求监控处理器
    /// </summary>
    public class DefatultRequestMonitorHandlers : IRequestMonitorHandlers
    {
        /// <summary>
        /// 处理器类型集合
        /// </summary>
        private readonly static List<Type> _handlerTypes = new();
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>() where T : class, IRequestMonitorHandler => AddHandler<T>();
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void AddHandler<T>() where T : class, IRequestMonitorHandler => _handlerTypes.Add(typeof(T));
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handlerType"></param>
        /// <exception cref="GatewayException"></exception>
        public static void AddHandler(Type handlerType)
        {
            if (handlerType.IsAssignableTo(typeof(IRequestMonitorHandler))) throw new GatewayException($"处理器必须实现{nameof(ICustomHandler)}");
            _handlerTypes.Add(handlerType);
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task HandlerRequstAsync(HandlerRequestModel model)
        {
            foreach (Type handlerType in _handlerTypes)
            {
                IRequestMonitorHandler handler = OcelotService.GetService<IRequestMonitorHandler>(handlerType);
                await handler.HandlerRequstAsync(model);
            }
        }
        /// <summary>
        /// 处理响应
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
