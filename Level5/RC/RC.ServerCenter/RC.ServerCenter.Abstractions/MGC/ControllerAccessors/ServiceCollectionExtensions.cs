using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RC.ServerCenter.Abstractions.ControllerAccessors
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
        public static void AddServerCenterControllerAccessors(this IServiceCollection services)
        {
            services.TryAddSingleton<IServerController, ServerControllerAccessor>();
            services.TryAddSingleton<INamespaceController, NamespaceControllerAccessor>();
            services.TryAddSingleton<IProjectController, ProjectControllerAccessor>();
        }
    }
}
