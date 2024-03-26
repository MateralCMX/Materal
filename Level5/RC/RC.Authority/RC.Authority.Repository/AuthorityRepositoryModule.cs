namespace RC.Authority.Repository
{
    /// <summary>
    /// Authority仓储模块
    /// </summary>
    public class AuthorityRepositoryModule() : RCRepositoryModule<AuthorityDBContext>("RC.Authority仓储模块", "RC.Authority.Repository")
    {
        /// <summary>
        /// 配置键
        /// </summary>
        protected override string ConfigKey => "Authority:DBConfig";
    }
}