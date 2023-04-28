using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加TTASqliteADONET仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbOption"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqliteADONETRepository<TDBOption>(this IServiceCollection services, TDBOption dbOption, params Assembly[] repositoryAssemblies)
            where TDBOption : DBOption
        {
            services.AddSingleton(dbOption);
            services.AddScoped(typeof(IRepositoryHelper<,>), typeof(SqliteRepositoryHelper<,>));
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
