namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter仓储模块
    /// </summary>
    public class ServerCenterRepositoryModule : RCRepositoryModule<ServerCenterDBContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServerCenterRepositoryModule() : base("RC.ServerCenter仓储模块", "RC.ServerCenter.Repository")
        {
        }
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "ServerCenter:DBConfig";
    }
}