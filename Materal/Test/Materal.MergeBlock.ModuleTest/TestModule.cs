namespace Materal.MergeBlock.ModuleTest
{
    public class TestModule() : MergeBlockModule("测试模块")
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILogger<TestModule>? logger = context.ServiceProvider.GetService<ILogger<TestModule>>();
            logger?.LogInformation("[测试模块]初始化中");
            base.OnApplicationInitialization(context);
        }
    }
}
