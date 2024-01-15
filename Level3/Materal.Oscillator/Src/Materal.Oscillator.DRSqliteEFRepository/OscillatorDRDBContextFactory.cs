using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.Oscillator.DRSqliteEFRepository
{
    /// <summary>
    /// 容灾数据库上下文工厂
    /// </summary>
    public class OscillatorDRDBContextFactory : IDesignTimeDbContextFactory<OscillatorDRDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorDRDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorDRDBContext>();
            SqliteConfigModel config = new()
            {
                Source = "OscillatorDR.db"
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new OscillatorDRDBContext(optionsBuilder.Options);

        }
    }
}
