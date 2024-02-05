namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy仓储模块
    /// </summary>
    public class DeployRepositoryModule : RCRepositoryModule<DeployDBContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DeployRepositoryModule() : base("RC.Deploy仓储模块", "RC.Deploy.Repository")
        {
        }
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "Deploy:DBConfig";
    }
}