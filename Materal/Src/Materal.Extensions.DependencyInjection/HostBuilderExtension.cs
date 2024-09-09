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
        public static IHostApplicationBuilder UseMateralServiceProvider(this IHostApplicationBuilder builder)
        {
            builder.ConfigureContainer(new MateralServiceContextProviderFactory());
            return builder;
        }

        /// <summary>
        /// 使用Materal容器
        /// </summary>
        /// <param name="builder"></param>
        public static IHostBuilder UseMateralServiceProvider(this IHostBuilder builder)
        {
            //#if NET8_0_OR_GREATER
            //            builder.ConfigureServices((content, services) =>
            //            {
            //                services.TryAddSingleton<IServiceProviderIsService, MateralServiceProviderIsService>();
            //            });
            //#endif
            builder.UseServiceProviderFactory(new MateralServiceContextProviderFactory());
            return builder;
        }
    }
}
