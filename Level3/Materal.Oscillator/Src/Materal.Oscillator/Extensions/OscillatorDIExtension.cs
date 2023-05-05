using Materal.Oscillator.Abstractions;
using Materal.Oscillator.QuartZExtend;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;

namespace Materal.Oscillator
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
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services, IConfiguration? configuration = null)
        {
            if (configuration != null)
            {
                OscillatorConfig.Init(configuration);
            }
            services.TryAddSingleton<IOscillatorHost, OscillatorHostImpl>();
            services.TryAddTransient<IJobListener, JobListener>();
            return services;
        }
    }
}
