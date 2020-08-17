using System;
using System.IO;
using System.Reflection;
using Materal.Common;
using Materal.ConDep.Center.Common;
using Materal.ConDep.Center.ServiceImpl;
using Materal.ConDep.Center.Services;
using Materal.ConDep.Center.SqliteEFRepository;
using Materal.ConDep.Filters;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.ConDep.Center.Server
{
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServer(this IServiceCollection services)
        {
            LogManager.Configuration.Variables["AppName"] = "ConDep";
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "NLog.config"));
            });
            FileHandler.HtmlPageFolderPath = "HtmlPages";
            MateralConfig.PageStartNumber = 1;
            services.AddDbContext<CenterDBContext>(options =>
            {
                options.UseSqlite(ApplicationConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<CenterDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConDep.Center.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConDep.Center.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddAutoMapperService(Assembly.Load("Materal.ConDep.Center.ServiceImpl"), Assembly.Load("Materal.ConDep.Center.Controllers"));
            services.AddSingleton<IServerManage, ServerManageImpl>();
            services.AddTransient<ICenterSqliteEFUnitOfWork, CenterSqliteEFUnitOfWorkImpl>();
            services.AddTransient<WebAPIHandler>();
            services.AddTransient<FileHandler>();
            services.AddControllerBus(options =>
            {
                options.AddFilter<ExceptionFilter>();
                options.AddFilter<AuthorityFilterAttribute>();
            }, Assembly.Load("Materal.ConDep.Center.Controllers"));
        }
    }
}
