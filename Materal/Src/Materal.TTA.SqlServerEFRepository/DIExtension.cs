using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加TTASqlServerEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, params Assembly[] repositoryAssemblies)
            where TDbConntext : DbContext => AddTTASqlServerEFRepository<TDbConntext>(services, dbConnectionString, null, repositoryAssemblies);
        /// <summary>
        /// 添加TTASqlServerEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, Action<SqlServerDbContextOptionsBuilder>? option)
            where TDbConntext : DbContext => AddTTASqlServerEFRepository<TDbConntext>(services, dbConnectionString, option, Array.Empty<Assembly>());
        /// <summary>
        /// 添加TTASqlServerEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="option"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, Action<SqlServerDbContextOptionsBuilder>? option, params Assembly[] repositoryAssemblies)
            where TDbConntext : DbContext
        {
            services.AddDbContext<TDbConntext>(options =>
            {
                options.UseSqlServer(dbConnectionString, option)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.TryAddScoped<IMigrateHelper<TDbConntext>, MigrateHelper<TDbConntext>>();
            services.AddTTARepository<TDbConntext>(repositoryAssemblies);
            return services;
        }
    }
}
