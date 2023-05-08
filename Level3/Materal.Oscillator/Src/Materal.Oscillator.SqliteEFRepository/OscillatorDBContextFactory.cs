using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.Oscillator.SqliteEFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class OscillatorDBContextFactory : IDesignTimeDbContextFactory<OscillatorDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorDBContext>();
            SqliteConfigModel config = new()
            {
                Source = "Oscillator.db"
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new OscillatorDBContext(optionsBuilder.Options);

        }
    }
}
