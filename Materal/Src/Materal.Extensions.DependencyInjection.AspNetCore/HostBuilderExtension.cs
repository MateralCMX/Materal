using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Extensions.DependencyInjection.AspNetCore
{
    /// <summary>
    /// 服务收集器扩展
    /// </summary>
    public static class HostBuilderExtension
    {
        /// <summary>
        /// 使用Materal服务提供者
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddMateralServiceProvider(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddSingleton<IServiceProviderIsService, MateralServiceProviderIsService>();
            builder.Host.UseServiceProviderFactory(new MateralServiceContextProviderFactory());
            return builder;
        }
    }
}
