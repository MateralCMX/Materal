namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB仓储模块
    /// </summary>
    public abstract class MMBRepositoryModule<TDBContext> : RepositoryModule<TDBContext, SqliteConfigModel>
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        public MMBRepositoryModule(string description, string[]? depends) : base(description, depends)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="moduleName"></param>
        /// <param name="depends"></param>
        public MMBRepositoryModule(string description, string? moduleName = null, string[]? depends = null) : base(description, moduleName, depends)
        {
        }
    }
}
