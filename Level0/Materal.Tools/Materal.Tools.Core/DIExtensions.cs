using Materal.Tools.Core.ChangeEncoding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Tools.Core
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// 添加Materal工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaateralTools(this IServiceCollection services)
        {
            services.TryAddSingleton<IChangeEncodingService, ChangeEncodingService>();
            return services;
        }
    }
}
