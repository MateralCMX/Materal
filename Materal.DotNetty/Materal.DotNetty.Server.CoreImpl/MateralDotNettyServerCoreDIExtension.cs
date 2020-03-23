using Materal.CacheHelper;
using Materal.DotNetty.Server.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.DotNetty.Server.CoreImpl
{
    public static class MateralDotNettyServerCoreDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddMateralDotNettyServerCore(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IDotNettyServer, DotNettyServerImpl>();
            services.AddTransient<ServerChannelHandler>();
        }
    }
}
