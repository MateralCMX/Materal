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
    /// <summary>
    /// Materal服务作用域工厂
    /// </summary>
    public class MateralServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Func<Type, Type, bool> _filter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralServiceScopeFactory(IServiceScopeFactory scopeFactory, Func<Type, Type, bool> filter)
        {
            _scopeFactory = scopeFactory;
            _filter = filter;
        }
        /// <summary>
        /// 创建作用域
        /// </summary>
        /// <returns></returns>
        public IServiceScope CreateScope()
        {
            IServiceScope serviceScope = _scopeFactory.CreateScope();
            IServiceScope result = new MateralServiceScope(serviceScope, _filter);
            return result;
        }
    }
    /// <summary>
    /// Materal服务作用域
    /// </summary>
    public sealed class MateralServiceScope : IServiceScope
    {
        private readonly IServiceScope _serviceScope;
        private readonly MateralServiceProvider _materalServiceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralServiceScope(IServiceScope serviceScope, Func<Type, Type, bool> filter)
        {
            _serviceScope = serviceScope;
            _materalServiceProvider = new MateralServiceProvider(_serviceScope.ServiceProvider, filter);
        }
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider => _materalServiceProvider;
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() => _serviceScope.Dispose();
    }
}
