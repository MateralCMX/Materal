using Materal.ConfigCenter.ProtalServer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository
{
    /// <summary>
    /// ProtalServer数据库上下文
    /// </summary>
    public sealed class ProtalServerDBContext : DbContext
    {
        public ProtalServerDBContext(DbContextOptions<ProtalServerDBContext> options) : base(options)
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
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
