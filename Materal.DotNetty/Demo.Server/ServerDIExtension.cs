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
            services.AddSingleton(new ServerConfig
            {
                Host = "192.168.0.101",
                Port = 8800
            });
        }
    }
}
