using Materal.Utils.Cache;
using Materal.Utils.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Utils
{
    /// <summary>
    /// 工具库DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加所有工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralUtils(this IServiceCollection services)
        {
            services.TryAddSingleton<ICacheHelper, MemoryCacheHelper>();
            services.TryAddSingleton<IHttpHelper, HttpHelper>();
            return services;
        }
    }
}
