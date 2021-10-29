using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.SqliteEFRepository;
using Materal.APP.Core;
using Materal.CacheHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace ConfigCenter.Environment.DependencyInjection
{
    public static class ConfigCenterEnvironmentDIExtension
    {
        public static void AddConfigCenterEnvironmentServices(this IServiceCollection services)
        {
            services.AddDbContext<ConfigCenterEnvironmentDBContext>(options =>
            {
                options.UseSqlite(ConfigCenterEnvironmentConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<ConfigCenterEnvironmentDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("ConfigCenter.Environment.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("ConfigCenter.Environment.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddTransient<IConfigCenterEnvironmentSqliteEFUnitOfWork, ConfigCenterEnvironmentSqliteEFUnitOfWorkImpl>();
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
