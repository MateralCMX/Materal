using Materal.TTA.Common.Model;
using Materal.TTA.SqlServerEFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.SqlServerEFRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlServer仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig">使用内存仓储</param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqlServerEFRepository(this IServiceCollection services, SqlServerConfigModel dbConfig)
        {
            services.AddTTASqlServerEFRepository<OscillatorDBContext>(dbConfig.ConnectionString);
            return services;
        }
    }
}
