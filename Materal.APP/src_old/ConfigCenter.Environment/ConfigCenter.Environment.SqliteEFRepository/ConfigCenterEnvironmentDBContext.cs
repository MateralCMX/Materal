using System.Reflection;
using ConfigCenter.Environment.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConfigCenter.Environment.SqliteEFRepository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public sealed class ConfigCenterEnvironmentDBContext : DbContext
    {
        public ConfigCenterEnvironmentDBContext(DbContextOptions<ConfigCenterEnvironmentDBContext> options) : base(options)
        {
        }
        /// <summary>
        /// 配置项
        /// </summary>
        public DbSet<ConfigurationItem> ConfigurationItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
