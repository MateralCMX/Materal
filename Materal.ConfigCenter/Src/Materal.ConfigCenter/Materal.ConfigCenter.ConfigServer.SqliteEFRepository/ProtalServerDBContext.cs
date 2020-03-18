using Materal.ConfigCenter.ConfigServer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.ConfigCenter.ConfigServer.SqliteEFRepository
{
    /// <summary>
    /// ConfigServer数据库上下文
    /// </summary>
    public sealed class ConfigServerDBContext : DbContext
    {
        public ConfigServerDBContext(DbContextOptions<ConfigServerDBContext> options) : base(options)
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
