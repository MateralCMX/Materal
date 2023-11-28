using Materal.TTA.Demo.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.TTA.Demo.MySqlEFRepository
{
    public class TTADemoDBContext : DbContext
    {
        public TTADemoDBContext() : base() { }
        public TTADemoDBContext(DbContextOptions<TTADemoDBContext> options) : base(options) { }
        public DbSet<TestDomain>? TestDomain { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.TTA.Demo.MySqlEFRepository"));
    }
}