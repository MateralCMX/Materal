using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.WPFUI
{
    public class WPFUIHelper
    {
        public static IServiceCollection Services;
        public static IServiceProvider ServiceProvider;
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterCustomerService()
        {
            Services = new ServiceCollection();
            Services.AddSingleton(ApplicationConfig.Configuration);
        }
        /// <summary>
        /// Build服务
        /// </summary>
        public static void BuildService()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
