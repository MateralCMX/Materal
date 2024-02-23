using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RC.Demo.Abstractions.ControllerAccessors
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
        public static void AddDemoControllerAccessors(this IServiceCollection services)
        {
            services.TryAddSingleton<IUserController, UserControllerAccessor>();
        }
    }
}
