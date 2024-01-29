using Materal.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.WebAPITest
{
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        public IServiceProvider CreateBuilder(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof(IControllerActivatorProvider), typeof(MyControllerActivatorProvider)));
            return services.BuildMateralServiceProvider();
        }

        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            if (containerBuilder is MateralServiceProvider) return containerBuilder;
            return new MateralServiceProvider(containerBuilder);
        }
    }
}