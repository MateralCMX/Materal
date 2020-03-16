using System.Reflection;
using Materal.DotNetty.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.ConfigCenter.Example
{
    public static class ClientDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServer(this IServiceCollection services)
        {
            //services.AddEventBus(Assembly.Load("Materal.ConfigCenter.ConfigServer.EventHandlers"));
        }
    }
}
