namespace Materal.MergeBlock.ModuleTest
{
    /// <summary>
    /// 测试模块
    /// </summary>
    [DependsOn(typeof(TestModule))]
    public class DependencyTestModule() : MergeBlockModule("依赖测试模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            MateralServices.Logger?.LogInformation("[依赖测试模块]配置中");
            base.OnConfigureServices(context);
        }
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
