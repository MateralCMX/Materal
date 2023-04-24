using Materal.Oscillator.DR.Domain;
using Microsoft.EntityFrameworkCore;

namespace Materal.Oscillator.DR
{
    public abstract class OscillatorDRDBContext<T> : DbContext
        where T : OscillatorDRDBContext<T>
    {
        protected OscillatorDRDBContext(DbContextOptions<T> options) : base(options) { }
        /// <summary>
        /// 流程
        /// </summary>
        public DbSet<Flow> Flow { get; set; }
    }
}
