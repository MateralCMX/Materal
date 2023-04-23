using Materal.TTA.Demo.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.TTA.Demo
{
    public class TTADemoDBContext : DbContext
    {
        public TTADemoDBContext(DbContextOptions<TTADemoDBContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)=> modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.TTA.Demo"));
    }
}
