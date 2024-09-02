﻿using AspectCore.Extensions.Hosting;
using Microsoft.Extensions.Hosting;
#if NET8_0_OR_GREATER
using Microsoft.Extensions.DependencyInjection.Extensions;
#endif

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// HostBuilder扩展
    /// </summary>
    public static partial class HostBuilderExtension
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
#if NET8_0_OR_GREATER
            builder.ConfigureServices(services =>
            {
                services.TryAddSingleton<IServiceProviderIsService, MateralServiceProviderIsService>();
            });
#endif
            builder.UseServiceContext();
        }
    }
}
