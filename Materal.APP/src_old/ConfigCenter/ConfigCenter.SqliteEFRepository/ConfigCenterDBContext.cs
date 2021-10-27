using ConfigCenter.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConfigCenter.SqliteEFRepository
{
    /// <summary>
    /// ProtalServer数据库上下文
    /// </summary>
    public sealed class ConfigCenterDBContext : DbContext
    {
        public ConfigCenterDBContext(DbContextOptions<ConfigCenterDBContext> options) : base(options)
        {
        }
        /// <summary>
        /// 命名空间
        /// </summary>
        public DbSet<Namespace> Namespace { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public DbSet<Project> Project { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
