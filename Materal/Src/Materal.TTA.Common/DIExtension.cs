using NetCore.AutoRegisterDi;

namespace Materal.TTA.Common
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加TTA仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTARepository(this IServiceCollection services, params Assembly[] repositoryAssemblies)
        {
            List<Assembly> repositoryAssemblyList = [.. repositoryAssemblies.Distinct()];
            foreach (Assembly repositoryAssembly in repositoryAssemblyList)
            {
                services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                    .Where(m => (m.IsAssignableTo<IRepository>() || m.IsAssignableTo<IUnitOfWork>()) && !m.IsAbstract)
                    .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            }
            return services;
        }
        /// <summary>
        /// 添加TTA仓储服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddTTARepository<T>(this IServiceCollection services, params Assembly[] repositoryAssemblies)
        {
            List<Assembly> repositoryAssemblyList = [.. repositoryAssemblies];
            repositoryAssemblyList.Add(typeof(T).Assembly);
            services.AddTTARepository([.. repositoryAssemblyList]);
            return services;
        }
    }
}
