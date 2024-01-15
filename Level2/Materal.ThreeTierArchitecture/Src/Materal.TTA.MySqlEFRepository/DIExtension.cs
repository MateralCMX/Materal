using Microsoft.Extensions.DependencyInjection.Extensions;
using MySql.EntityFrameworkCore.Infrastructure;

namespace Materal.TTA.MySqlEFRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加TTAMySqlEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTAMySqlEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, params Assembly[] repositoryAssemblies)
            where TDbConntext : DbContext => AddTTAMySqlEFRepository<TDbConntext>(services, dbConnectionString, null, repositoryAssemblies);
        /// <summary>
        /// 添加TTAMySqlEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTAMySqlEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, Action<MySQLDbContextOptionsBuilder>? option)
            where TDbConntext : DbContext => AddTTAMySqlEFRepository<TDbConntext>(services, dbConnectionString, option, Array.Empty<Assembly>());
        /// <summary>
        /// 添加TTAMySqlEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="option"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTAMySqlEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, Action<MySQLDbContextOptionsBuilder>? option, params Assembly[] repositoryAssemblies)
            where TDbConntext : DbContext
        {
            services.AddDbContext<TDbConntext>(options =>
            {
                options.UseMySQL(dbConnectionString, option)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.TryAddScoped<IMigrateHelper<TDbConntext>, MigrateHelper<TDbConntext>>();
            services.AddTTARepository<TDbConntext>(repositoryAssemblies);
            return services;
        }
    }
}
