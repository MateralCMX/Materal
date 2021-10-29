using ConfigCenter.Common;
using ConfigCenter.SqliteEFRepository;
using Materal.APP.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace ConfigCenter.DependencyInjection
{
    public static class ConfigCenterDIExtension
    {
        public static void AddConfigCenterServices(this IServiceCollection services)
        {
            services.AddDbContext<ConfigCenterDBContext>(options =>
            {
                options.UseSqlite(ConfigCenterConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<ConfigCenterDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("ConfigCenter.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl") && !c.Name.Equals("ConfigCenterServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("ConfigCenter.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddTransient<IConfigCenterSqliteEFUnitOfWork, ConfigCenterSqliteEFUnitOfWorkImpl>();
        }
    }
}
