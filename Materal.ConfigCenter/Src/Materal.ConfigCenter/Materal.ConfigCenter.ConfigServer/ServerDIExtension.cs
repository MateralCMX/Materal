using System.Reflection;
using Materal.DotNetty.CommandBus;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddCommandBus(Assembly.Load("Materal.ConfigCenter.ConfigServer.CommandHandlers"));
        }
    }
}
