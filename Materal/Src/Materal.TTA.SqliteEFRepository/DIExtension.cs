using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.TTA.SqliteEFRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加TTASqliteEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqliteEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, params Assembly[] repositoryAssemblies)
            where TDbConntext : DbContext => AddTTASqliteEFRepository<TDbConntext>(services, dbConnectionString, null, repositoryAssemblies);
        /// <summary>
        /// 添加TTASqliteEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqliteEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, Action<SqliteDbContextOptionsBuilder>? option)
            where TDbConntext : DbContext => AddTTASqliteEFRepository<TDbConntext>(services, dbConnectionString, option, Array.Empty<Assembly>());
        /// <summary>
        /// 添加TTASqliteEF仓储服务
        /// </summary>
        /// <typeparam name="TDbConntext"></typeparam>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="option"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqliteEFRepository<TDbConntext>(this IServiceCollection services, string dbConnectionString, Action<SqliteDbContextOptionsBuilder>? option, params Assembly[] repositoryAssemblies)
            where TDbConntext : DbContext
        {
            void ConfigDbContext(DbContextOptionsBuilder options)
            {
                options.UseSqlite(dbConnectionString, option);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            services.AddDbContext<TDbConntext>(ConfigDbContext);
            services.TryAddScoped<IMigrateHelper<TDbConntext>, MigrateHelper<TDbConntext>>();
            services.AddTTARepository<TDbConntext>(repositoryAssemblies);
            return services;
        }
    }
}
