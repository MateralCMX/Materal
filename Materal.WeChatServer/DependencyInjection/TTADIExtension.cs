using Materal.CacheHelper;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    /// <summary>
    /// MateralTTA依赖注入
    /// </summary>
    public static class TTADIExtension
    {
        /// <summary>
        /// 添加TTA服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddTTAServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<ICacheManager, MemoryCacheManager>();
        }
    }
}
