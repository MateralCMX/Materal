using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RC.EnvironmentServer.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加控制器访问器
        /// </summary>
        /// <param name="services"></param>
        public static void AddEnvironmentServerControllerAccessors(this IServiceCollection services)
        {
            services.TryAddSingleton<IConfigurationItemController, ConfigurationItemControllerAccessor>();
        }
    }
}
