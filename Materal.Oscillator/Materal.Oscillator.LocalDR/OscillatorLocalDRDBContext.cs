using Materal.Oscillator.DR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.Oscillator.LocalDR
{
    public sealed class OscillatorLocalDRDBContext : OscillatorDRDBContext<OscillatorLocalDRDBContext>
    {
        public OscillatorLocalDRDBContext(DbContextOptions<OscillatorLocalDRDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.Oscillator.LocalDR"));
    }
}
