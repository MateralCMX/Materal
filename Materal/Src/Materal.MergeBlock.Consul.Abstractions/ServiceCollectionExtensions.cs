using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Consul.Abstractions
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Consul配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceName"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, string serviceName, params string[] tags)
        {
            services.AddConsulConfig(serviceName, tags, null);
            return services;
        }
        /// <summary>
        /// 添加Consul配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceName"></param>
        /// <param name="tags"></param>
        /// <param name="healthPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, string serviceName, string[] tags, string? healthPath)
        {
            ModuleConsulConfig consulConfig = new() { ServiceName = serviceName, Tags = tags, HealthPath = healthPath };
            services.AddSingleton(consulConfig);
            return services;
        }
    }
}
