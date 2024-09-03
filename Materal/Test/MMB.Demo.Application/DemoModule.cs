using Materal.MergeBlock.Abstractions.Extensions;

namespace MMB.Demo.Application
{
    public class DemoModule() : MergeBlockModule("Demo模块")
    {
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHostedServiceWithDecorators<MyService>();
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILogger<DemoModule>? logger = context.ServiceProvider.GetService<ILogger<DemoModule>>();
            logger?.LogInformation("[Demo模块]初始化中");
        }
    }
}
