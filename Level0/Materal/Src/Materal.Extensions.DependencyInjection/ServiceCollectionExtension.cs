namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务收集器扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 构建Materal服务提供者
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider BuildMateralServiceProvider(this IServiceCollection services) 
            => services.BuildServiceContextProvider();
    }
}
