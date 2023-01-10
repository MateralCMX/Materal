using MBC.Demo.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MBC.Demo.SqliteEFRepository
{
    public class DemoDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options"></param>
        public DemoDBContext(DbContextOptions<DemoDBContext> options) : base(options) { }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("MBC.Demo.SqliteEFRepository"));
    }
}
