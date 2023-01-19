using Microsoft.EntityFrameworkCore;
using System.Reflection;
using RC.Authority.Domain;

namespace RC.Authority.EFRepository
{
    /// <summary>
    /// Authority数据库上下文
    /// </summary>
    public sealed partial class AuthorityDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public AuthorityDBContext(DbContextOptions<AuthorityDBContext> options) : base(options) { }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
