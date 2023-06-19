using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加TTASqlServerADONET仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerADONETRepository<TDBOption>(this IServiceCollection services, TDBOption dbOption)
            where TDBOption : DBOption
        {
            services.AddSingleton(dbOption);
            services.TryAddScoped<IMaigrateRepository, SqlServerMaigrateRepositoryImpl>();
            services.AddScoped(typeof(IRepositoryHelper<,>), typeof(SqlServerRepositoryHelper<,>));
            services.TryAddScoped<IMigrateHelper<TDBOption>, MigrateHelper<TDBOption>>();
            return services;
        }
        /// <summary>
        /// 添加TTASqlServerADONET仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerADONETRepository<TDBOption>(this IServiceCollection services, TDBOption dbOption, Assembly[] repositoryAssemblies)
            where TDBOption : DBOption
        {
            services.AddTTASqlServerADONETRepository(dbOption);
            services.AddTTARepository<TDBOption>(repositoryAssemblies);
            return services;
        }
    }
}
