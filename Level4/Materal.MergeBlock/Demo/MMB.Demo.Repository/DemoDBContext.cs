using Microsoft.EntityFrameworkCore;
using MMB.Demo.Domain;
using System.Reflection;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo数据库上下文
    /// </summary>
    public sealed partial class DemoDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DemoDBContext(DbContextOptions<DemoDBContext> options) : base(options) { }
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
