using Materal.Extensions.DependencyInjection;

namespace Materal.BaseCore.WebAPI
{
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        public IServiceProvider CreateBuilder(IServiceCollection services) => services.BuildMateralServiceProvider();
        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder) => containerBuilder;
    }
}