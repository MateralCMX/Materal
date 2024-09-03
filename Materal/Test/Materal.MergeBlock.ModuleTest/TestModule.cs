using Materal.MergeBlock.Abstractions.Extensions;

namespace Materal.MergeBlock.ModuleTest
{
    public class TestModule() : MergeBlockModule("测试模块")
    {
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHostedServiceWithDecorators<MyService>();
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILogger<TestModule>? logger = context.ServiceProvider.GetService<ILogger<TestModule>>();
            logger?.LogInformation("[测试模块]初始化中");
        }
    }
}
