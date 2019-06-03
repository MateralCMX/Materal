using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Log.EFRepository
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 日志
        /// </summary>
        public DbSet<Domain.Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
