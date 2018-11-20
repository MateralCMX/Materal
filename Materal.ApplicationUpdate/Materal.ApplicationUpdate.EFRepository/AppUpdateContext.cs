using Materal.ApplicationUpdate.Domain;
using Microsoft.EntityFrameworkCore;

namespace Materal.ApplicationUpdate.EFRepository
{
    public class AppUpdateContext : DbContext
    {
        public AppUpdateContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
