namespace Materal.Utils.Consul
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Consul工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralConsulUtils(this IServiceCollection services)
        {
            services.AddMateralUtils();
            services.TryAddSingleton<IConsulService, ConsulServiceImpl>();
            return services;
        }
    }
}
