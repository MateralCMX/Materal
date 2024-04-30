using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.Tools.Core;
using Materal.Utils.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Tools.Test
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider ServiceProvider;
        public BaseTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMateralUtils();
            services.AddMateralLogger(options =>
            {
                options.AddConsoleTarget("ConsoleLogger").AddAllTargetsRule();
            }, true);
            services.AddMateralTools();
            OnConfig(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        protected virtual void OnConfig(IServiceCollection services) { }
    }
}
