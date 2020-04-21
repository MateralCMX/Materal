using System;
using System.IO;
using Materal.ConDep.Controllers.Filters;
using Materal.ConDep.ServiceImpl;
using Materal.ConDep.Services;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.ConDep.Server
{
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServer(this IServiceCollection services)
        {
            FileHandler.HtmlPageFolderPath = "Application";
            services.AddMemoryCache();
            LogManager.Configuration.Variables["AppName"] = "ConDep";
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config"));
            });
            services.AddSingleton<IAppService, AppServiceImpl>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConDep.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl") && c.Name != "AppServiceImpl")
                .AsPublicImplementedInterfaces();
            services.AddTransient<WebAPIHandler>();
            services.AddTransient<ConDepFileHandler>();
            services.AddControllerBus(controllerHelper =>
            {
                controllerHelper.AddFilter<ExceptionFilter>();
                controllerHelper.AddFilter<AuthorityFilterAttribute>();
            }, Assembly.Load("Materal.ConDep.Controllers"));
        }
    }
}
