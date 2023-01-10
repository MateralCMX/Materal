using RC.Demo.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RC.Demo.EFRepository
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
        ///  用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
