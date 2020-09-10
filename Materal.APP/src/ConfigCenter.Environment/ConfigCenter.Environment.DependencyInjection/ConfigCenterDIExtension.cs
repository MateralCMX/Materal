using System.Reflection;
using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.HubImpl.ConfigCenterHub;
using ConfigCenter.Environment.SqliteEFRepository;
using ConfigCenter.Hubs.Clients;
using ConfigCenter.Hubs.Hubs;
using Materal.APP.Core;
using Materal.APP.HttpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

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
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("ConfigCenter.Environment.HttpClientImpl"))
                .Where(c => c.Name.EndsWith("HttpClientImpl"))
                .AsPublicImplementedInterfaces();
            services.AddSingleton<IAuthorityManage, DefaultAuthorityManageImpl>();
            services.AddSingleton<IConfigCenterClient, ConfigCenterClientImpl>();
            services.AddSingleton<IConfigCenterHub, ConfigCenterHubImpl>();
            services.AddTransient<IConfigCenterEnvironmentSqliteEFUnitOfWork, ConfigCenterEnvironmentSqliteEFUnitOfWorkImpl>();
        }
    }
}
