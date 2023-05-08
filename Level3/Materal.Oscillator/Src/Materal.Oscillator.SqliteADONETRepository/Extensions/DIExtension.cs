using Materal.TTA.SqliteADONETRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.SqliteADONETRepository
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlite仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqliteADONETRepository(this IServiceCollection services, OscillatorDBOption dbOption)
        {
            services.AddTTASqliteADONETRepository(dbOption);
            services.AddScoped<OscillatorUnitOfWorkImpl>();
            return services;
        }
    }
}
