﻿namespace Materal.Extensions.DependencyInjection
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
        public static IHostApplicationBuilder AddMateralServiceProvider(this IHostApplicationBuilder builder)
        {
            builder.ConfigureContainer(new MateralServiceContextProviderFactory());
            return builder;
        }
        /// <summary>
        /// 使用Materal服务提供者
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IHost UseMateralServiceProvider(this IHost app) => app;
    }
}