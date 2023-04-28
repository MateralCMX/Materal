using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCore.AutoRegisterDi;
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
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerADONETRepository<TDBOption>(this IServiceCollection services, TDBOption dbOption, params Assembly[] repositoryAssemblies)
            where TDBOption : DBOption
        {
            services.AddSingleton(dbOption);
            services.TryAddScoped<IMaigrateRepository, SqlServerMaigrateRepositoryImpl>();
            services.AddScoped(typeof(IRepositoryHelper<,>), typeof(SqlServerRepositoryHelper<,>));
            services.TryAddScoped<IMigrateHelper<TDBOption>, MigrateHelper<TDBOption>>();
            foreach (Assembly repositoryAssembly in repositoryAssemblies)
            {
                services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                    .Where(m => (m.IsAssignableTo<IRepository>() || m.IsAssignableTo<IUnitOfWork>()) && !m.IsAbstract)
                    .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            }
            return services;
        }
    }
}
