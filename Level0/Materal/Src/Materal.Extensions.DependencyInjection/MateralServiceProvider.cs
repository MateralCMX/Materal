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
        /// <param name="serviceProvider"></param>
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
            if (obj is not null)
            {
                obj = DecoratorBuilder.BuildDecoratorObject(obj);
            }
            return obj;
        }
    }
}
