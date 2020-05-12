using Materal.ConfigCenter.ConfigServer.Controllers.Filters;
using Materal.ConfigCenter.ConfigServer.ServiceImpl;
using Materal.ConfigCenter.ConfigServer.Services;
using Materal.ConfigCenter.ControllerCore.Filters;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Materal.Common;
using Materal.ConfigCenter.ConfigServer.Common;
using Microsoft.EntityFrameworkCore;
using Materal.ConfigCenter.ConfigServer.SqliteEFRepository;

namespace Materal.ConfigCenter.ConfigServer
{
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServer(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
            services.AddDbContext<ConfigServerDBContext>(options =>
            {
                options.UseSqlite(ApplicationConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<ConfigServerDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConfigCenter.ConfigServer.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl") && !c.Name.Equals("ConfigServerServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.AddSingleton<IConfigServerService, ConfigServerServiceImpl>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConfigCenter.ConfigServer.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConfigCenter.ConfigServer.HttpRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddAutoMapperService(Assembly.Load("Materal.ConfigCenter.ConfigServer.ServiceImpl"));
            services.AddTransient<IConfigServerUnitOfWork, ConfigServerSqliteEFUnitOfWorkImpl>();
            services.AddTransient<WebAPIHandler>();
            services.AddControllerBus(options =>
            {
                options.AddFilter<ExceptionFilter>();
                options.AddFilter<AuthorityFilterAttribute>();
            }, Assembly.Load("Materal.ConfigCenter.ConfigServer.Controllers"));
        }
    }
}
