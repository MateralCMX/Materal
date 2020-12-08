using Materal.CacheHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.WebSocketClient.CoreImpl
{
    public static class MateralWebSocketClientCoreDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddMateralWebSocketClientCore(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
