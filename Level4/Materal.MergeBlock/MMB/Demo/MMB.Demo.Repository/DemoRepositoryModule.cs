namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo仓储模块
    /// </summary>
    public class DemoRepositoryModule : MMBRepositoryModule<DemoDBContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DemoRepositoryModule() : base("MMB.Demo仓储模块", "MMB.Demo.Repository")
        {
        }
    }
}
