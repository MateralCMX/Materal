using Materal.Oscillator.DR;
using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.LocalDR
{
    /// <summary>
    /// 调度器本地容灾DI扩展
    /// </summary>
    public static class OscillatorLocalDRDIExtension
    {
        /// <summary>
        /// 添加Oscillator本地容灾服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorLocalDR(this IServiceCollection services, SqliteConfigModel dbConfig)
        {
            services.AddTTASqliteEFRepository<OscillatorLocalDRDBContext>(dbConfig.ConnectionString);
            services.TryAddSingleton<IOscillatorDR, OscillatorDRImpl>();
            return services;
        }
    }
}
