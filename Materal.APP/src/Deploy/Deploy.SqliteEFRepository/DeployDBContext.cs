using System.Reflection;
using Deploy.Domain;
using Microsoft.EntityFrameworkCore;

namespace Deploy.SqliteEFRepository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public sealed class DeployDBContext : DbContext
    {
        public DeployDBContext(DbContextOptions<DeployDBContext> options) : base(options)
        {
        }
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public DbSet<ApplicationInfo> ApplicationInfo { get; set; }
        /// <summary>
        /// 默认数据
        /// </summary>
        public DbSet<DefaultData> DefaultData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
