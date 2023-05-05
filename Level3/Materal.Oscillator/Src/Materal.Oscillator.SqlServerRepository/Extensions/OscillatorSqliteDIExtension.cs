using Materal.Oscillator.SqlServerRepository;
using Materal.TTA.Common.Model;
using Materal.TTA.SqlServerEFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class OscillatorSqlServerDIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlServer仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig">使用内存仓储</param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqlServerRepository(this IServiceCollection services, SqlServerConfigModel dbConfig)
        {
            services.AddTTASqlServerEFRepository<OscillatorSqlServerDBContext>(dbConfig.ConnectionString);
            return services;
        }
    }
}
