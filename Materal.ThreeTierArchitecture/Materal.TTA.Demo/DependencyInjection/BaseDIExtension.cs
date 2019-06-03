using Common;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    /// <summary>
    /// 基础依赖注入扩展类
    /// </summary>
    public static class BaseDIExtension
    {
        /// <summary>
        /// 添加基础服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddBaseServices(this IServiceCollection services)
        {
            services.AddSingleton(ApplicationConfig.Configuration);
            services.AddLogServices();
            services.AddTTAServices();
        }
    }
}
