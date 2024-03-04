/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerAccessorServiceCollectionExtensions
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RC.Authority.Abstractions.ControllerAccessors
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
        public static void AddAuthorityControllerAccessors(this IServiceCollection services)
        {
            services.TryAddSingleton<IUserController, UserControllerAccessor>();
        }
    }
}
