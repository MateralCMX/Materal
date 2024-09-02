namespace Materal.MergeBlock.ModuleTest
{
    /// <summary>
    /// 测试模块
    /// </summary>
    [DependsOn(typeof(TestModule))]
    public class DependencyTestModule() : MergeBlockModule("依赖测试模块")
    {
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILogger<DependencyTestModule>? logger = context.ServiceProvider.GetService<ILogger<DependencyTestModule>>();
            logger?.LogInformation("[依赖测试模块]初始化中");
            base.OnApplicationInitialization(context);
        }
    }
}
