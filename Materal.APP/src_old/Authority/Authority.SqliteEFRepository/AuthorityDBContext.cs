using System.Reflection;
using Authority.Domain;
using Microsoft.EntityFrameworkCore;

namespace Authority.SqliteEFRepository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public sealed class AuthorityDBContext : DbContext
    {
        public AuthorityDBContext(DbContextOptions<AuthorityDBContext> options) : base(options)
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
