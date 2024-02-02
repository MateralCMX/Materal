namespace RC.Demo.Repository
{
    /// <summary>
    /// Demo仓储模块
    /// </summary>
    public class DemoRepositoryModule : RCRepositoryModule<DemoDBContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DemoRepositoryModule() : base("RC.Demo仓储模块", "RC.Demo.Repository")
        {
        }
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "RC.Demo:DBConfig";
    }
}