using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.SqliteADONETRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCore.AutoRegisterDi;
using System.Reflection;

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
        /// <typeparam name="TUnitOfWork"></typeparam>
        /// <param name="services"></param>
        /// <param name="getUnitOfWork"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerEFRepository<TUnitOfWork>(this IServiceCollection services, Func<IServiceProvider, TUnitOfWork> getUnitOfWork, params Assembly[] repositoryAssemblies)
            where TUnitOfWork : SqliteADONETUnitOfWorkImpl, IUnitOfWork
        {
            services.TryAddScoped(typeof(IRepositoryHelper<,>), typeof(SqliteRepositoryHelper<,>));
            services.TryAddScoped(typeof(IUnitOfWork), getUnitOfWork);
            services.TryAddScoped<IUnitOfWork, TUnitOfWork>();
            foreach (Assembly repositoryAssembly in repositoryAssemblies)
            {
                services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                    .Where(m => m.IsAssignableTo<IRepository>() && !m.IsAbstract)
                    .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            }
            return services;
        }
        /// <summary>
        /// 添加TTASqlServerEF仓储服务
        /// </summary>
        /// <typeparam name="TUnitOfWork"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="services"></param>
        /// <param name="getUnitOfWork"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerEFRepository<TUnitOfWork, TPrimaryKeyType>(this IServiceCollection services, Func<IServiceProvider, TUnitOfWork> getUnitOfWork, params Assembly[] repositoryAssemblies)
            where TUnitOfWork : SqliteADONETUnitOfWorkImpl, IUnitOfWork<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            AddTTASqlServerEFRepository(services, getUnitOfWork, repositoryAssemblies);
            services.TryAddScoped(typeof(IUnitOfWork<TPrimaryKeyType>), getUnitOfWork);
            return services;
        }
        /// <summary>
        /// 添加TTASqlServerEF仓储服务
        /// </summary>
        /// <typeparam name="TIUnitOfWork"></typeparam>
        /// <typeparam name="TUnitOfWork"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="services"></param>
        /// <param name="getUnitOfWork"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTASqlServerEFRepository<TIUnitOfWork, TUnitOfWork, TPrimaryKeyType>(this IServiceCollection services, Func<IServiceProvider, TUnitOfWork> getUnitOfWork, params Assembly[] repositoryAssemblies)
            where TIUnitOfWork : class, IUnitOfWork<TPrimaryKeyType>
            where TUnitOfWork : SqliteADONETUnitOfWorkImpl, TIUnitOfWork, IUnitOfWork<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            AddTTASqlServerEFRepository<TUnitOfWork, TPrimaryKeyType>(services, getUnitOfWork, repositoryAssemblies);
            services.TryAddScoped(typeof(TIUnitOfWork), getUnitOfWork);
            return services;
        }
    }
}
