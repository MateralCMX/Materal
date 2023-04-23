using Microsoft.EntityFrameworkCore;
using System.Reflection;
using RC.EnvironmentServer.Domain;

namespace RC.EnvironmentServer.EFRepository
{
    /// <summary>
    /// EnvironmentServer数据库上下文
    /// </summary>
    public sealed partial class EnvironmentServerDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EnvironmentServerDBContext(DbContextOptions<EnvironmentServerDBContext> options) : base(options) { }
        /// <summary>
        /// 配置项
        /// </summary>
        public DbSet<ConfigurationItem> ConfigurationItem { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
