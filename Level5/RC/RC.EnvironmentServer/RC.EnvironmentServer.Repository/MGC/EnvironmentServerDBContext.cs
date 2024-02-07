namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer数据库上下文
    /// </summary>
    public sealed partial class EnvironmentServerDBContext(DbContextOptions<EnvironmentServerDBContext> options) : DbContext(options)
    {
        /// <summary>
        /// 配置项
        /// </summary>
        public DbSet<ConfigurationItem>? ConfigurationItem { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
