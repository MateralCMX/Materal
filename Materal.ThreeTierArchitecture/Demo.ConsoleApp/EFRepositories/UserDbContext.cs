using Demo.ConsoleApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.ConsoleApp.EFRepositories
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
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
