using Materal.Oscillator.DR;
using Materal.TTA.SqlServerADONETRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Oscillator.DRSqlServerADONETRepository
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加OscillatorDRSqlServer仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorDRSqlServerADONETRepository(this IServiceCollection services, OscillatorDRDBOption dbOption)
        {
            services.AddTTASqlServerADONETRepository(dbOption);
            services.AddScoped<OscillatorDRUnitOfWorkImpl>();
            services.TryAddSingleton<IOscillatorDR, OscillatorDRImpl>();
            return services;
        }
    }
}
