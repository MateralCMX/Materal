using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.ContextCache
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加上下文缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddContextCache<TPersistence>(this IServiceCollection services)
            where TPersistence : class, IContextCachePersistence
        {
            services.AddContextCache();
            services.TryAddSingleton<IContextCachePersistence, TPersistence>();
            return services;
        }
        /// <summary>
        /// 添加上下文缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddContextCacheByLocalFile(this IServiceCollection services)
        {
            services.AddContextCache<ContextCacheFilePersistence>();
            return services;
        }
        /// <summary>
        /// 添加上下文缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddContextCache(this IServiceCollection services)
        {
            services.TryAddSingleton<IContextCacheService, ContextCacheService>();
            return services;
        }
    }
}
