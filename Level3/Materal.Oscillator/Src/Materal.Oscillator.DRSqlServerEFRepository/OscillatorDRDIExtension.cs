using Materal.Oscillator.DR;
using Materal.TTA.Common.Model;
using Materal.TTA.SqlServerEFRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.DRSqlServerEFRepository
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
        public static IServiceCollection AddOscillatorDRSqlServerRepository(this IServiceCollection services, SqlServerConfigModel dbConfig)
        {
            services.AddTTASqlServerEFRepository<OscillatorDRDBContext>(dbConfig.ConnectionString);
            services.TryAddSingleton<IOscillatorDR, OscillatorDRImpl>();
            return services;
        }
    }
}
