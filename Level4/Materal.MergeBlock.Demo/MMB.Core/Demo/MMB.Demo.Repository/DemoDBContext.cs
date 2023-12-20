namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo数据库上下文
    /// </summary>
    public sealed partial class DemoDBContext(DbContextOptions<DemoDBContext> options) : DbContext(options)
    {
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
