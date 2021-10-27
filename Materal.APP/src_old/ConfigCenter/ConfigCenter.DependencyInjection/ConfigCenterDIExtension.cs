using ConfigCenter.Common;
using ConfigCenter.HubImpl.ServerHub;
using ConfigCenter.SqliteEFRepository;
using Materal.APP.Core;
using Materal.APP.Hubs.Clients;
using Materal.APP.Hubs.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using ConfigCenter.ServiceImpl;
using ConfigCenter.Services;

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
            services.AddSingleton<IServerClient, ServerClientImpl>();
            services.AddSingleton<IServerHub, ServerHubImpl>();
            services.AddSingleton<IConfigCenterService, ConfigCenterServiceImpl>();
            services.AddTransient<IConfigCenterSqliteEFUnitOfWork, ConfigCenterSqliteEFUnitOfWorkImpl>();
        }
    }
}
