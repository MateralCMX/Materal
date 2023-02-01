using Materal.ConDep.Center.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.ConDep.Center.SqliteEFRepository
{
    /// <summary>
    /// ProtalServer数据库上下文
    /// </summary>
    public sealed class CenterDBContext : DbContext
    {
        public CenterDBContext(DbContextOptions<CenterDBContext> options) : base(options)
        {
        }
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
