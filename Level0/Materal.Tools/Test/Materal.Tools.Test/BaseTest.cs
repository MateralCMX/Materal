using Microsoft.Extensions.DependencyInjection;

namespace Materal.Tools.Test
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider ServiceProvider;
        public BaseTest()
        {
            IServiceCollection services = new ServiceCollection();
            OnConfig(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        protected virtual void OnConfig(IServiceCollection services) { }
    }
}
