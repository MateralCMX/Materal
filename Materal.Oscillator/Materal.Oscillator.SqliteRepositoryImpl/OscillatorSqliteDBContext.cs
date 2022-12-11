using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.Oscillator.Abstractions
{
    public sealed class OscillatorSqliteDBContext : OscillatorDBContext<OscillatorSqliteDBContext>
    {
        public OscillatorSqliteDBContext(DbContextOptions<OscillatorSqliteDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.Oscillator.SqliteRepositoryImpl"));
    }
}
