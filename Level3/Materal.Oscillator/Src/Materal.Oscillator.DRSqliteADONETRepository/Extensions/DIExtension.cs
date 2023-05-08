using Materal.Oscillator.DR;
using Materal.TTA.SqliteADONETRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.DRSqliteADONETRepository
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加OscillatorDRSqlite仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorDRSqliteADONETRepository(this IServiceCollection services, OscillatorDRDBOption dbOption)
        {
            services.AddTTASqliteADONETRepository(dbOption);
            services.AddScoped<OscillatorDRUnitOfWorkImpl>();
            services.TryAddSingleton<IOscillatorDR, OscillatorDRImpl>();
            return services;
        }
    }
}
