using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// 服务提供者工厂
    /// </summary>
    public class WebModuleMateralServiceProviderFactory : MateralServiceProviderFactory
    {
        /// <summary>
        /// 创建构建器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public override IServiceProvider CreateBuilder(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof(IControllerActivatorProvider), typeof(MergeBlockControllerActivatorProvider)));
            return base.CreateBuilder(services);
        }
    }
}