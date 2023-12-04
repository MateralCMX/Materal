using Materal.Utils.Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Utils
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// 添加Consul工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulUtils(this IServiceCollection services)
        {
            services.AddMateralUtils();
            services.TryAddSingleton<IConsulService, ConsulServiceImpl>();
            return services;
        }
    }
}
