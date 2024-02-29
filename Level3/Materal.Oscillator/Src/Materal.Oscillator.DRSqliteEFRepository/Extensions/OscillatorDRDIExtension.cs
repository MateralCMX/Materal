using Materal.Oscillator.Abstractions.DR;
using Materal.Oscillator.DR;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.DRSqliteEFRepository.Extensions
{
    /// <summary>
    /// 调度器容灾DI扩展
    /// </summary>
    public static class OscillatorDRDIExtension
    {
        /// <summary>
        /// 添加Oscillator容灾服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorDRSqliteEFRepository(this IServiceCollection services, SqliteConfigModel dbConfig)
        {
            services.AddTTASqliteEFRepository<OscillatorDRDBContext>(dbConfig.ConnectionString);
            services.TryAddSingleton<IOscillatorDR, OscillatorDRImpl>();
            return services;
        }
    }
}
