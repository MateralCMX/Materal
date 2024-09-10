using Microsoft.AspNetCore.Builder;

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
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddMateralServiceProvider(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new MateralServiceContextProviderFactory());
            builder.Services.AddSingleton<ReplaceServiceProviderMiddleware>();
            return builder;
        }
        /// <summary>
        /// 使用Materal服务提供者
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMateralServiceProvider(this WebApplication app)
        {
            app.UseMiddleware<ReplaceServiceProviderMiddleware>();
            return app;
        }
    }
}
