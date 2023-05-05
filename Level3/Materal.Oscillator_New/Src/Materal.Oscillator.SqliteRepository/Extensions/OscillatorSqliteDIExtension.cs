using Materal.Oscillator.SqliteRepository;
using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.Oscillator
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class OscillatorSqliteDIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlite仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig">使用内存仓储</param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqliteRepository(this IServiceCollection services, SqliteConfigModel dbConfig)
        {
            services.AddTTASqliteEFRepository<OscillatorDBContext>(dbConfig.ConnectionString, Assembly.Load("Materal.Oscillator.SqliteRepository"));
            return services;
        }
    }
}
