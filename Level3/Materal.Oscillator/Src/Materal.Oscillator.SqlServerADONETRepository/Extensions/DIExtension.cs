using Materal.TTA.SqlServerADONETRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.SqlServerADONETRepository
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlServer仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqlServerADONETRepository(this IServiceCollection services, OscillatorDBOption dbOption)
        {
            services.AddTTASqlServerADONETRepository(dbOption);
            services.AddScoped<OscillatorUnitOfWorkImpl>();
            return services;
        }
    }
}
