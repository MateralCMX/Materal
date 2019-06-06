using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeChatService.Domain;
namespace WeChatService.EFRepository
{
    /// <summary>
    /// WeChatService数据库上下文
    /// </summary>
    public sealed class WeChatServiceDbContext : DbContext
    {
        public WeChatServiceDbContext(DbContextOptions<WeChatServiceDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 应用
        /// </summary>
        public DbSet<Application> Application { get; set; }
        /// <summary>
        /// 微信域名
        /// </summary>
        public DbSet<WeChatDomain> WeChatDomain { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
