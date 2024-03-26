namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer仓储模块
    /// </summary>
    public class EnvironmentServerRepositoryModule() : RCRepositoryModule<EnvironmentServerDBContext>("RC.EnvironmentServer仓储模块", "RC.EnvironmentServer.Repository")
    {
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "EnvironmentServer:DBConfig";
    }
}