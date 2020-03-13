using System.Reflection;
using Demo.Controllers.Filters;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Server
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
            services.AddTransient<WebAPIHandler>();
            services.AddTransient<FileHandler>();
            services.AddControllerBus(controllerHelper =>
            {
                controllerHelper.AddFilter<ExceptionFilter>();
            }, Assembly.Load("Demo.Controllers"));
        }
    }
}
