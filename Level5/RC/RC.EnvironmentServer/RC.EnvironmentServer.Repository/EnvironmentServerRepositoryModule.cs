namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer仓储模块
    /// </summary>
    public class EnvironmentServerRepositoryModule : RCRepositoryModule<EnvironmentServerDBContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EnvironmentServerRepositoryModule() : base("RC.EnvironmentServer仓储模块", "RC.EnvironmentServer.Repository")
        {
        }
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "RC.EnvironmentServer:DBConfig";
    }
}