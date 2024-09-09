namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal容器上下文提供者工厂
    /// </summary>
    public class MateralServiceContextProviderFactory() : IServiceProviderFactory<IServiceCollection>
    {
        /// <summary>
        /// 创建构建器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection CreateBuilder(IServiceCollection services) => services;
        /// <summary>
        /// 创建服务提供者
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider CreateServiceProvider(IServiceCollection services) => services.BuildMateralServiceProvider();
    }
}
