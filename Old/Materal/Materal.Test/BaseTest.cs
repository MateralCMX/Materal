using Materal.Abstractions;
using Materal.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Test
{
    public abstract class BaseTest
    {
        private IServiceCollection _serviceCollection;
        protected IServiceProvider Services;
        protected BaseTest()
        {
            _serviceCollection = new ServiceCollection();
            MateralConfig.PageStartNumber = 1;
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            AddConfig(configurationBuilder);
            HttpMessageHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, ceti, chain, errors) => true
            };
            HttpClient httpClient = new(handler);
            _serviceCollection.TryAddSingleton(httpClient);
            _serviceCollection.AddMateralUtils();
            AddServices(_serviceCollection);
            MateralServices.Services = _serviceCollection.BuildServiceProvider();
            Services = MateralServices.Services;
        }
        public virtual void AddServices(IServiceCollection services)
        {

        }
        public virtual void AddConfig(IConfigurationBuilder builder)
        {
        }
    }
}
