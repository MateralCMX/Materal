/*
 * Generator Code From MateralMergeBlock=>GeneratorDBContextCodeAsync
 */
namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy数据库上下文
    /// </summary>
    public sealed partial class DeployDBContext(DbContextOptions<DeployDBContext> options) : DbContext(options)
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public DbSet<ApplicationInfo>? ApplicationInfo { get; set; }
        /// <summary>
        /// 默认数据
        /// </summary>
        public DbSet<DefaultData>? DefaultData { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
