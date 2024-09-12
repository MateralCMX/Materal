using Materal.MergeBlock.Abstractions.Extensions;
using Materal.MergeBlock.Consul.Abstractions;

namespace MMB.Demo.Application
{
    public class DemoModule() : MergeBlockModule("Demo模块")
    {
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            ModuleConsulConfig moduleConsulConfig = new()
            {
                ServiceName = "Demo模块",
                Tags = ["Demo", "Demo模块"]
            };
            context.Services.AddSingleton(moduleConsulConfig);
            context.Services.AddMergeBlockHostedService<MyService>();
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILogger<DemoModule>? logger = context.ServiceProvider.GetService<ILogger<DemoModule>>();
            logger?.LogInformation("[Demo模块]初始化中");
        }
    }
}
