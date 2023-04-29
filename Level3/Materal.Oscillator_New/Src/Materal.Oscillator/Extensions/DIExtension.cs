using Materal.Oscillator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.Extensions
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services)
        {
            services.TryAddSingleton<IOscillatorHost, OscillatorHostImpl>();
            return services;
        }
    }
}
