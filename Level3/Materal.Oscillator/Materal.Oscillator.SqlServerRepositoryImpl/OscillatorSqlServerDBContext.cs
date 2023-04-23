using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.Oscillator.Abstractions
{
    public sealed class OscillatorSqlServerDBContext : OscillatorDBContext<OscillatorSqlServerDBContext>
    {
        public OscillatorSqlServerDBContext(DbContextOptions<OscillatorSqlServerDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.Oscillator.SqlServerRepositoryImpl"));
    }
}
