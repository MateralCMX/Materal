using Materal.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BaseCore.WebAPI
{
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        public IServiceProvider CreateBuilder(IServiceCollection services) => services.BuildMateralServiceProvider();

        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            if (containerBuilder is MateralServiceProvider) return containerBuilder;
            return new MateralServiceProvider(containerBuilder);
        }
    }
}