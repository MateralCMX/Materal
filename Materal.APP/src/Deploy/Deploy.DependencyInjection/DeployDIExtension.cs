using Deploy.Common;
using Deploy.HubImpl.ServerHub;
using Deploy.ServiceImpl.Manage;
using Deploy.SqliteEFRepository;
using Materal.APP.Core;
using Materal.APP.Hubs.Clients;
using Materal.APP.Hubs.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Deploy.DependencyInjection
{
    public static class DeployDIExtension
    {
        public static void AddDeployServices(this IServiceCollection services)
        {
            services.AddDbContext<DeployDBContext>(options =>
            {
                options.UseSqlite(DeployConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<DeployDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Deploy.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Deploy.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddSingleton<IServerClient, ServerClientImpl>();
            services.AddSingleton<IServerHub, ServerHubImpl>();
            services.AddSingleton<IApplicationManage, ApplicationManageImpl>();
            services.AddTransient<IDeploySqliteEFUnitOfWork, DeploySqliteEFUnitOfWorkImpl>();
        }
    }
}
