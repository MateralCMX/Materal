using Materal.ConfigCenter.ControllerCore.Filters;
using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.Controllers.Filters;
using Materal.ConfigCenter.ProtalServer.ServiceImpl;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConfigCenter.ProtalServer.SqliteEFRepository;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Materal.Common;

namespace Materal.ConfigCenter.ProtalServer
{
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServer(this IServiceCollection services)
        {
            FileHandler.HtmlPageFolderPath = "HtmlPages";
            MateralConfig.PageStartNumber = 1;
            services.AddDbContext<ProtalServerDBContext>(options =>
            {
                options.UseSqlite(ApplicationConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<ProtalServerDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConfigCenter.ProtalServer.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl") && !c.Name.Equals("ConfigServerServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.AddSingleton<IConfigServerService, ConfigServerServiceImpl>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConfigCenter.ProtalServer.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConfigCenter.ProtalServer.HttpRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddAutoMapperService(Assembly.Load("Materal.ConfigCenter.ProtalServer.ServiceImpl"));
            services.AddTransient<IProtalServerUnitOfWork, ProtalServerSqliteEFUnitOfWorkImpl>();
            services.AddTransient<WebAPIHandler>();
            services.AddTransient<FileHandler>();
            services.AddControllerBus(options =>
            {
                options.AddFilter<ExceptionFilter>();
                options.AddFilter<AuthorityFilterAttribute>();
            }, Assembly.Load("Materal.ConfigCenter.ProtalServer.Controllers"));
        }
    }
}
