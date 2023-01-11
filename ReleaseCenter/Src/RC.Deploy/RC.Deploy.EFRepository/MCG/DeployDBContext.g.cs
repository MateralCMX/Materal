using Microsoft.EntityFrameworkCore;
using System.Reflection;
using RC.Deploy.Domain;

namespace RC.Deploy.EFRepository
{
    /// <summary>
    /// Deploy数据库上下文
    /// </summary>
    public sealed partial class DeployDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DeployDBContext(DbContextOptions<DeployDBContext> options) : base(options) { }
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public DbSet<ApplicationInfo> ApplicationInfo { get; set; }
        /// <summary>
        /// 默认数据
        /// </summary>
        public DbSet<DefaultData> DefaultData { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
