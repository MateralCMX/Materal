namespace Materal.MergeBlock.ModuleTest
{
    public class TestModule() : MergeBlockModule("测试模块")
    {
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            MateralServices.Logger?.LogInformation("[测试模块]配置中");
            base.OnConfigureServices(context);
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILogger<TestModule>? logger = context.ServiceProvider.GetService<ILogger<TestModule>>();
            logger?.LogInformation("[测试模块]初始化中");
            base.OnApplicationInitialization(context);
        }
    }
}
