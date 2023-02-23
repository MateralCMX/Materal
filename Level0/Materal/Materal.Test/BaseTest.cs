using Materal.Abstractions;
using Materal.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Test
{
    public abstract class BaseTest
    {
        private IServiceCollection _serviceCollection;
        protected IServiceProvider Services;
        public BaseTest()
        {
            _serviceCollection = new ServiceCollection();
            MateralConfig.PageStartNumber = 1;
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            AddConfig(configurationBuilder);
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
