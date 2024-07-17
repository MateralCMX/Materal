using Materal.ContextCache;
using Materal.ContextCache.SqlitePersistence;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.Extensions
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class OscillatorDIExtension
    {
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services)
        {
            services.AddOscillator<ContextCacheSqlitePersistence>();
            return services;
        }
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator<TContextCachePersistence>(this IServiceCollection services)
            where TContextCachePersistence : class, IContextCachePersistence
        {
            services.TryAddSingleton<IOscillatorHost, OscillatorHost>();
            services.AddContextCache<TContextCachePersistence>();
            return services;
        }
        /// <summary>
        /// 使用Oscillator
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task<IServiceProvider> UseOscillatorAsync(this IServiceProvider services)
        {
            OscillatorServices.ServiceProvider = services;
            IOscillatorHost oscillatorHost = services.GetRequiredService<IOscillatorHost>();
            await oscillatorHost.StartAsync();
            return services;
        }
    }
}
