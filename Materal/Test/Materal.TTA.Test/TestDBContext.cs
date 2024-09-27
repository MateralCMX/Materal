using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Materal.TTA.Test
{
    public class TestDBContext(DbContextOptions<TestDBContext> options) : DbContext(options)
    {
        [MemberNotNull]
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 配置模型
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
