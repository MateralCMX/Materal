using Microsoft.Extensions.DependencyInjection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务提供者
    /// </summary>
    public class MateralServiceProvider : IServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Func<Type, Type, bool> _filter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralServiceProvider(IServiceProvider serviceProvider, Func<Type, Type, bool>? filter = null)
        {
            _serviceProvider = serviceProvider;
            _filter = filter ?? ((serviceType, objType) => true);
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object? GetService(Type serviceType)
        {
            object? obj = _serviceProvider.GetService(serviceType);
            if (obj is null) return obj;
            if (obj is IServiceScopeFactory serviceScopeFactory && serviceType == typeof(IServiceScopeFactory)) return new MateralServiceScopeFactory(serviceScopeFactory, _filter);
            if (!serviceType.IsInterface || !_filter(serviceType, obj.GetType())) return obj;
            object result = DecoratorBuilder.BuildDecoratorObject(obj, _serviceProvider);
            return result;
        }
    }
}
