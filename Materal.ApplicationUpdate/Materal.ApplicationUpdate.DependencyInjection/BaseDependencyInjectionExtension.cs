using System.Reflection;
using Materal.ApplicationUpdate.Common;
using Materal.ApplicationUpdate.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace Materal.ApplicationUpdate.DependencyInjection
{
    public static class BaseDependencyInjectionExtension
    {
        /// <summary>
        /// 添加基础依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddBaseServices(this IServiceCollection services)
        {
            services.AddDbContext<AppUpdateContext>(options => options.UseSqlite(ApplicationConfig.Configuration["ConnectionStrings:ApplicationUpdateDB"]), ServiceLifetime.Transient);

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ApplicationUpdate.EFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ApplicationUpdate.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
        }
    }
}
