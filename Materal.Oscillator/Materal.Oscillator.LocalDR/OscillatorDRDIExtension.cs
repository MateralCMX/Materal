using Materal.Oscillator.DR;
using Materal.Oscillator.LocalDR;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator
{
    public static class OscillatorDRDIExtension
    {
        /// <summary>
        /// 添加Oscillator本地容灾服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorLocalDRService(this IServiceCollection services)
        {
            services.AddTransient<IOscillatorDR, OscillatorLocalDR>();
            return services;
        }
    }
}
