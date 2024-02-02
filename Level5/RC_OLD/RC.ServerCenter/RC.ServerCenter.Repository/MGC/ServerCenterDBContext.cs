namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter数据库上下文
    /// </summary>
    public sealed partial class ServerCenterDBContext(DbContextOptions<ServerCenterDBContext> options) : DbContext(options)
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public DbSet<Namespace> Namespace { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public DbSet<Project> Project { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
