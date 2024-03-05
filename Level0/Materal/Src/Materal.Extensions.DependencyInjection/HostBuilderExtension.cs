using AspectCore.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// HostBuilder扩展
    /// </summary>
    public static class HostBuilderExtension
    {
        /// <summary>
        /// 使用Materal容器
        /// </summary>
        /// <param name="builder"></param>
        public static void UseMateralServiceProvider(this IHostApplicationBuilder builder)
        {
            builder.ConfigureContainer(new ServiceContextProviderFactory());
        }
        /// <summary>
        /// 使用Materal容器
        /// </summary>
        /// <param name="builder"></param>
        public static void UseMateralServiceProvider(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.TryAddSingleton<IServiceProviderIsService, MyServiceProviderIsService>();
            });
            builder.UseServiceContext();
        }
        /// <summary>
        /// 服务提供者是否是服务
        /// </summary>
        public class MyServiceProviderIsService : IServiceProviderIsService
        {
            /// <summary>
            /// 是否是服务
            /// </summary>
            /// <param name="serviceType"></param>
            /// <returns></returns>
            public bool IsService(Type serviceType) => true;
        }
    }
}
