using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.SqliteEFRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlite仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig">使用内存仓储</param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqliteEFRepository(this IServiceCollection services, SqliteConfigModel dbConfig)
        {
            services.AddTTASqliteEFRepository<OscillatorDBContext>(dbConfig.ConnectionString);
            return services;
        }
    }
}
