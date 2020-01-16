using Materal.CacheHelper;
using Materal.ConDep.Controllers.Filters;
using Materal.DotNetty.CommandBus;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

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
            FileHandler.HtmlPageFolderPath = "HtmlPages";
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConDep.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.AddControllerBus(controllerHelper =>
            {
                controllerHelper.AddFilter<ExceptionFilter>();
                controllerHelper.AddFilter<AuthorityFilterAttribute>();
            }, Assembly.Load("Materal.ConDep.Controllers"));
            services.AddCommandBus(Assembly.Load("Materal.ConDep.CommandHandlers"));
        }
    }
}
