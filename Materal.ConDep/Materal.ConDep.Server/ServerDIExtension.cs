using Materal.CacheHelper;
using Materal.ConDep.Controllers.Filters;
using Materal.ConDep.ServiceImpl;
using Materal.ConDep.Services;
using Materal.DotNetty.ControllerBus;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Materal.DotNetty.Server.CoreImpl;

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
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
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
