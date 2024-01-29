namespace Materal.MergeBlock
{
    /// <summary>
    /// 服务提供者工厂
    /// </summary>
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        /// <summary>
        /// 创建构建器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceProvider CreateBuilder(IServiceCollection services) => services.BuildMateralServiceProvider();
        /// <summary>
        /// 创建服务提供者
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <returns></returns>
        public virtual IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder) => containerBuilder is MateralServiceProvider ? containerBuilder : new MateralServiceProvider(containerBuilder);
    }
}