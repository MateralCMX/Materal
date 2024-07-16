using Microsoft.Extensions.DependencyInjection;

namespace Materal.ContextCache.SqlitePersistence
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
        public static IServiceCollection AddContextCacheBySqlite(this IServiceCollection services)
        {
            services.AddContextCache<ContextCacheSqlitePersistence>();
            return services;
        }
    }
}
