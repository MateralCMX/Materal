namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务提供者
    /// </summary>
    public class MateralServiceProvider : IServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object? GetService(Type serviceType)
        {
            object? obj = _serviceProvider.GetService(serviceType);
            if (!serviceType.IsInterface || obj is null) return obj;
            object result = DecoratorBuilder.BuildDecoratorObject(obj, _serviceProvider);
            return result;
        }
    }
}
