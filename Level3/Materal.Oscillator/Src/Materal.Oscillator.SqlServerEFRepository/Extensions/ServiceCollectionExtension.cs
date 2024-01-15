namespace Materal.Oscillator.SqlServerEFRepository.Extensions
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class ServiceCollectionExtension
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
