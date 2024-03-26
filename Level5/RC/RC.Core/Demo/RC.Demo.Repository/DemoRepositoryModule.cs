namespace RC.Demo.Repository
{
    /// <summary>
    /// Demo仓储模块
    /// </summary>
    public class DemoRepositoryModule() : RCRepositoryModule<DemoDBContext>("RC.Demo仓储模块", "RC.Demo.Repository")
    {
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "Demo:DBConfig";
    }
}