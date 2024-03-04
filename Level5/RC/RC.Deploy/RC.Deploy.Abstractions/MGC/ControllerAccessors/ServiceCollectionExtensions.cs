/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerAccessorServiceCollectionExtensions
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RC.Deploy.Abstractions.ControllerAccessors
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
        public static void AddDeployControllerAccessors(this IServiceCollection services)
        {
            services.TryAddSingleton<IApplicationInfoController, ApplicationInfoControllerAccessor>();
            services.TryAddSingleton<IDefaultDataController, DefaultDataControllerAccessor>();
        }
    }
}
